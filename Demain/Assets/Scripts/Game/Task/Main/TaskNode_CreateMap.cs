using System;
using System.Collections.Generic;
using UnityEngine;
namespace game {
    public class TaskNode_CreateMap : TaskNodeBase {
        #region Room
        class Room {
            public int Index { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int X { get; private set; }
            public int Y { get; private set; }
            /// <summary>道を作る際に起点となるX座標</summary>
            public int RoadPointX { get; private set; }
            /// <summary>道を作る際に起点となるY座標</summary>
            public int RoadPointY { get; private set; }

            public Room(int index, int x, int y, int width, int height) {
                Index = index;
                X = x;
                Y = y;
                Width = width;
                Height = height;

                RoadPointX = UnityEngine.Random.Range(X, X + width);
                RoadPointX = Math.Min(RoadPointX, GameDefine.FLOOR_MAX_WIDTH - 1);
                RoadPointY = UnityEngine.Random.Range(Y, Y + Height);
                RoadPointY = Math.Min(RoadPointY, GameDefine.FLOOR_MAX_HEIGHT - 1);
            }

            //public void ConvertRoomIndex(int index) {
            //    Index = index;
            //    foreach (var grid in m_grids) {
            //        grid = index;
            //    }
            //}
        }
        #endregion
        #region BlockParam
        class BlockParam {
            public BlockType Type { get; private set; }
            public bool Untouchable { get; private set; }
            public int RoomID { get; private set; }

            public BlockParam(BlockType type, bool untouchable) {
                this.Type = type;
                this.Untouchable = untouchable;
                this.RoomID = -1;   //!< 無効値
            }

            public void ChangeType(BlockType type, int roomId = -1) {
                this.Type = type;
                this.RoomID = roomId;
            }
        }
        #endregion

        List<List<BlockParam>> m_mapSheet = null;
        List<Room> m_rooms = null;

        public override bool Setup() {
            int mapWidth = GameDefine.FLOOR_MAX_WIDTH + GameDefine.UNTOUCHABLE_AREA;
            int mapHeight = GameDefine.FLOOR_MAX_HEIGHT + GameDefine.UNTOUCHABLE_AREA;

            m_mapSheet = new List<List<BlockParam>>();
            // まずシートを壁で埋める
            for (int x = 0; x < mapWidth; ++x) {
                bool untouchable = false;
                m_mapSheet.Add(new List<BlockParam>());
                for(int y = 0; y < mapHeight; ++y) {
                    untouchable = ((x < GameDefine.UNTOUCHABLE_AREA) || (y < GameDefine.UNTOUCHABLE_AREA) || (x >= (mapWidth - GameDefine.UNTOUCHABLE_AREA)) || (y >= (mapHeight - GameDefine.UNTOUCHABLE_AREA)));
                    m_mapSheet[x].Add(new BlockParam(BlockType.Wall, untouchable));
                }
            }

            // 部屋を数個ランダムで作る
            int roomCount = UnityEngine.Random.Range(GameDefine.MIN_ROOM_COUNT, GameDefine.MAX_ROOM_COUTN);
            m_rooms = new List<Room>();
            for (int i = 0; i < roomCount; ++i) {
                // 部屋のサイズをランダムに決める
                int roomWidth = UnityEngine.Random.Range(GameDefine.MIN_ROOM_WIDTH, GameDefine.MAX_ROOM_WIDTH);
                int roomHeight = UnityEngine.Random.Range(GameDefine.MIN_ROOM_HEIGHT, GameDefine.MAX_ROOM_HEIGHT);
                // 部屋の原点位置(左上)をランダムに決める
                int roomBasePosX = (UnityEngine.Random.Range(0, mapWidth) - (GameDefine.MAX_ROOM_WIDTH / 3));
                int roomBasePosY = UnityEngine.Random.Range(0, mapHeight) - (GameDefine.MAX_ROOM_HEIGHT / 3);
                roomBasePosX = Math.Max(roomBasePosX, 0);
                roomBasePosY = Math.Max(roomBasePosY, 0);

                m_rooms.Add(new Room(i, roomBasePosX, roomBasePosY, roomWidth, roomHeight));
            }

            // 実際に部屋を作る
            int size = m_rooms.Count;
            for(int roomID = 0; roomID < size; ++roomID) {
                var room = m_rooms[roomID];
                for (int i = 0; i < room.Width; ++i) {
                    int x = room.X + i;
                    if (m_mapSheet.Count <= x) { continue; }
                    for (int j = 0; j < room.Height; ++j) {
                        int y = room.Y + j;
                        if (m_mapSheet[x].Count <= y) { continue; }
                        // 床だけの情報にする
                        var blockParam = m_mapSheet[x][y];
                        if (blockParam.Untouchable) { continue; }
                        blockParam.ChangeType(BlockType.NormalFloor, roomID);
                    }
                }
            }

            { // 階段を作る
                bool bRet = false;
                while (!bRet) {
                    int roomID = UnityEngine.Random.Range(0, size);
                    var room = m_rooms[roomID];
                    int x = UnityEngine.Random.Range(room.X, room.X + room.Width);
                    int y = UnityEngine.Random.Range(room.Y, room.Y + room.Height);
                    if (m_mapSheet.Count <= x) { continue; }
                    if (m_mapSheet[x].Count <= y) { continue; }

                    var blockParam = m_mapSheet[x][y];
                    if (blockParam.RoomID == roomID &&
                        !blockParam.Untouchable) {
                        blockParam.ChangeType(BlockType.Stairs, roomID);
                        bRet = true;
                    }
                }
            }
            // 通路を作る
            foreach (var room in m_rooms) {
                // 道をつなげる対象の部屋の数
                int maxCount = (m_rooms.Count >= 2) ? 2 : m_rooms.Count;
                int targetRoomCount = UnityEngine.Random.Range(1, maxCount);
                List<Room> targetRooms = new List<Room>();

                // 重複しない様に対象の部屋を幾つかピックアップ
                while (targetRoomCount > 0) {
                    int i = UnityEngine.Random.Range(0, m_rooms.Count);
                    var CheckRoom = m_rooms[i];
                    bool insert = true;
                    if (CheckRoom.Index == room.Index) { continue; }
                    foreach (var r in targetRooms) {
                        if (r.Index == CheckRoom.Index) {
                            insert = false;
                            break;
                        }
                    }
                    if (insert) {
                        targetRooms.Add(CheckRoom);
                        --targetRoomCount;
                    }
                }

                foreach (var targetRoom in targetRooms) {
                    // 縦の道を作る
                    int diff = Math.Abs(room.RoadPointY - targetRoom.RoadPointY);
                    int y = room.RoadPointY;
                    while (diff > 0) {
                        --diff;

                        y = (room.RoadPointY < targetRoom.RoadPointY) ? ++y : --y;
                        if (y >= m_mapSheet[room.RoadPointX].Count) { continue; }
                        var blockParam = m_mapSheet[room.RoadPointX][y];
                        if (blockParam.Untouchable) { continue; }
                        blockParam.ChangeType(BlockType.NormalFloor);
                    }

                    // 横に道を作る
                    diff = Math.Abs(room.RoadPointX - targetRoom.RoadPointX);
                    int x = room.RoadPointX;
                    while (diff > 0) {
                        --diff;

                        x = (room.RoadPointX < targetRoom.RoadPointX) ? ++x : --x;
                        if (x >= m_mapSheet.Count) { continue; }
                        var blockParam = m_mapSheet[x][y];
                        if (blockParam.Untouchable) { continue; }
                        blockParam.ChangeType(BlockType.NormalFloor);
                    }
                }
            }
            return true;
        }

        public override bool Exec() {
            // ブロック生成
            int column = m_mapSheet.Count;
            for(int x = 0; x < column; ++x) {
                int line = m_mapSheet[x].Count;
                for(int y = 0; y < line; ++y) {
                    var blockParam = m_mapSheet[x][y];
                    if(blockParam.Type == BlockType.Stairs) 
                    {// 階段を作る
                        var blockPrefab = (GameObject)Resources.Load("Game/Block/Block_Stairs");
                        if (blockPrefab != null) {
                            var block = GameObject.Instantiate(blockPrefab, BlockManager.Instance.gameObject.transform);
                            var module = block.GetComponent<BlockBase>();
                            if (module != null) {
                                module.Initialize(BlockType.Stairs, x, 0, y);
                                if (blockParam.RoomID != -1) {
                                    module.SetRoomParam(blockParam.RoomID);
                                }
                            }
                            BlockManager.Instance.AddBlock(block);
                        }
                    }
                    else if(blockParam.Type == BlockType.NormalFloor)
                    {// 床だけ作る
                        var blockPrefab = (GameObject)Resources.Load("Game/Block/Block_NormalFloor");
                        if (blockPrefab != null) {
                            var block = GameObject.Instantiate(blockPrefab, BlockManager.Instance.gameObject.transform);
                            var module = block.GetComponent<BlockBase>();
                            if (module != null) {
                                module.Initialize(BlockType.NormalFloor, x, 0, y);
                                if(blockParam.RoomID != -1) {
                                    module.SetRoomParam(blockParam.RoomID);
                                }
                            }
                            BlockManager.Instance.AddBlock(block);
                        }
                    }
                    else if(blockParam.Type == BlockType.Wall) 
                    {// 床の上に壁ブロックを設置する
                        var floorPref = (GameObject)Resources.Load("Game/Block/Block_NormalFloor");
                        var wallPref = (blockParam.Untouchable) ? (GameObject)Resources.Load("Game/Block/Block_UntachableWall") : (GameObject)Resources.Load("Game/Block/Block_Wall");
                        if (floorPref != null && wallPref != null) {
                            var floor = GameObject.Instantiate(floorPref, BlockManager.Instance.gameObject.transform);
                            var wall = GameObject.Instantiate(wallPref, BlockManager.Instance.gameObject.transform);
                            var floorModule = floor.GetComponent<BlockBase>();
                            var wallModulle = wall.GetComponent<BlockBase>();
                            if (floorModule != null && wallModulle != null) {
                                floorModule.Initialize(BlockType.NormalFloor, x, 0, y);
                                wallModulle.Initialize(BlockType.Wall, x, 1, y);
                                floorModule.SetBlockOnAbove(wall);
                            }
                            BlockManager.Instance.AddBlock(floor);
                            BlockManager.Instance.AddBlock(wall);
                        }
                    }
                }
            }

            return true;
        }

        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_SpawnPlayer());
            return true;
        }
    }
}