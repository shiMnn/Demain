using UnityEngine;

namespace game {
    public class BlockBase : MonoBehaviour {
        public int MapIndex_X { get; protected set; }
        public int MapIndex_Y { get; protected set; }

        public void Initialize(int x, int y) {
            this.MapIndex_X = x;
            this.MapIndex_Y = y;
            gameObject.transform.position =
                new Vector3(GameDefine.BLOCK_SIZE_WIDTH * MapIndex_X,
                            0.0f,
                            GameDefine.BLOCK_SIZE_DEPTH * MapIndex_Y);
        }
    }
}