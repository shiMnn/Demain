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

        public void AllRelease() {
            int size = m_blocks.Count;
            for(int i = 0; i < size; ++i) {
                var block = m_blocks[i];
                if (block != null) {
                    GameObject.Destroy(block.gameObject);
                }
            }

            m_blocks.Clear();
        }

        /// <summary>
        /// 指定座標のブロックを取得する
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="posZ"></param>
        /// <returns>null返却の可能性あり</returns>
        public GameObject GetBlock(int posX, int posY, int posZ) {
            foreach(var block in m_blocks) {
                var module = block.GetComponent<BlockBase>();
                if (module != null) {
                    if( module.MapIndex_X == posX &&
                        module.MapIndex_Y == posY &&
                        module.MapIndex_Z == posZ) {
                        return block;
                    }
                }
            }
            return null;
        }
    }
}