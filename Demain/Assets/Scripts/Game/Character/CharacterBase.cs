using System;
using System.Collections.Generic;
using UnityEngine;
namespace game {
    public class CharacterBase : MonoBehaviour {
        public bool Controlling { get; protected set; }

        public int MapIndex_X { get; protected set; }
        public int MapIndex_Y { get; protected set; }
        public int MapIndex_Z { get; protected set; }

        protected Dictionary<ActionType, ActionBase> m_actions = new Dictionary<ActionType, ActionBase>();

        private void Update() {
            // 各種アクションの更新
            foreach(var action in m_actions) {
                action.Value.Update();
            }
        }

        public virtual void Initialize(int x, int y, int z) {
            this.SetMapIndex(x, y, z);
        }

        public void SetMapIndex(int x, int y, int z) {
            this.MapIndex_X = x;
            this.MapIndex_Y = y;
            this.MapIndex_Z = z;

            gameObject.transform.position =
                new Vector3(GameDefine.BLOCK_SIZE_WIDTH * MapIndex_X,
                            GameDefine.BLOCK_SIZE_HEIGHT * MapIndex_Y,
                            GameDefine.BLOCK_SIZE_DEPTH * MapIndex_Z);
        }

        /// <summary>
        /// アクション取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns>null返却の可能性あり</returns>
        public ActionBase GetAction(ActionType type) {
            ActionBase action = null;
            m_actions.TryGetValue(type, out action);

            return action;
        }
    }
}