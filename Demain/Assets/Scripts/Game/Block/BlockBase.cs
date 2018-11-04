using UnityEngine;

namespace game {
    public class BlockBase : MonoBehaviour {
        public int MapIndex_X { get; protected set; }
        public int MapIndex_Y { get; protected set; }
        public int MapIndex_Z { get; protected set; }

        public BlockType BlockType { get; protected set; }

        private GameObject m_aboveBlock = null;

        public bool IsRoom { get; private set; }
        public int RoomID { get; private set; }

        public void Initialize(BlockType type, int x, int y, int z) {
            this.BlockType = type;

            this.MapIndex_X = x;
            this.MapIndex_Y = y;
            this.MapIndex_Z = z;
            gameObject.transform.position =
                new Vector3(GameDefine.BLOCK_SIZE_WIDTH * MapIndex_X,
                            GameDefine.BLOCK_SIZE_HEIGHT * MapIndex_Y,
                            GameDefine.BLOCK_SIZE_DEPTH * MapIndex_Z);
        }

        public void SetBlockOnAbove(GameObject block) {
            var module = block.GetComponent<BlockBase>();
            if(module != null) {
                m_aboveBlock = block;
            }
        }

        /// <summary>
        /// 上にブロックが乗っている？
        /// </summary>
        /// <returns></returns>
        public bool IsOnAbove() {
            return (m_aboveBlock != null);
        }

        /// <summary>
        /// 上のブロックを取得する
        /// </summary>
        /// <returns>null返却の可能性あり</returns>
        public GameObject GetBlockOnAbove() {
            return m_aboveBlock;
        }

        public void SetRoomParam(int id) {
            this.RoomID = id;
            IsRoom = true;
        }
    }
}