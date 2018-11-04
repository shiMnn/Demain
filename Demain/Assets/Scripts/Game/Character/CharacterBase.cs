using UnityEngine;
namespace game {
    public class CharacterBase : MonoBehaviour {
        public bool Controlling { get; protected set; }

        public int MapIndex_X { get; protected set; }
        public int MapIndex_Y { get; protected set; }

        private void Update() {
            gameObject.transform.position =
                new Vector3(GameDefine.BLOCK_SIZE_WIDTH * MapIndex_X,
                            GameDefine.BLOCK_SIZE_HEIGHT,
                            GameDefine.BLOCK_SIZE_DEPTH * MapIndex_Y);
        }

        public virtual void Initialize(int x, int y) {
            this.MapIndex_X = x;
            this.MapIndex_Y = y;
        }

        public void SetTargetPos(int targetPosX, int targetPosY) {
            this.MapIndex_X = targetPosX;
            this.MapIndex_Y = targetPosY;
        }
    }
}