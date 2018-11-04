using System.Collections.Generic;
using UnityEngine;
namespace game {
    public class TaskNode_CreateMap : TaskNodeBase {
        List<List<int>> m_mapSheet = null;

        public override bool Setup() {
            m_mapSheet = new List<List<int>>();
            // マップシートを成形
            for(int x = 0; x < 4; ++x) {
                m_mapSheet.Add(new List<int>());
                for(int y = 0; y < 4; ++y) {
                    m_mapSheet[x].Add(0);
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
                    int value = m_mapSheet[x][y];
                    if(value == 0) {
                        var blockPrefab = (GameObject)Resources.Load("Game/Block/Block_NormalFloor");
                        if (blockPrefab != null) {
                            var block = GameObject.Instantiate(blockPrefab, BlockManager.Instance.gameObject.transform);
                            var module = block.GetComponent<BlockBase>();
                            if (module != null) {
                                module.Initialize(x, y);
                            }
                            BlockManager.Instance.AddBlock(block);
                        }
                    }
                }
            }

            return true;
        }

        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_PlayPhase());
            return true;
        }
    }
}