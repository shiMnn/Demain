using System.Collections.Generic;
using UnityEngine;

namespace game {
    public class BlockManager : SingletonMonoBehaviour<BlockManager> {
        List<GameObject> m_blocks;

        public BlockManager() {
            m_blocks = new List<GameObject>();
        }

        public void AddBlock(GameObject block) {
            m_blocks.Add(block);
        }
    }
}