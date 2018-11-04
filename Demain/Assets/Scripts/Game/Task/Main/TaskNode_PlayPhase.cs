using UnityEngine;

namespace game {
    public class TaskNode_PlayPhase : TaskNodeBase {
        public override bool Setup() {
            return true;
        }
        public override bool Exec() {
            Direction direction = Direction.None;
            if (Input.GetKey(KeyCode.W)) {
                direction = Direction.Top;
            }
            if (Input.GetKey(KeyCode.S)) {
                direction = Direction.Bottom;
            }
            if (Input.GetKey(KeyCode.A)) {
                direction = Direction.Left;
            }
            if (Input.GetKey(KeyCode.D)) {
                direction = Direction.Right;
            }
            if (direction != Direction.None) {
                TaskManager.Instance.Next(this, new TaskNode_Move(direction));
                return true;
            }
            return false;
        }
        public override bool Finish() {
            return true;
        }
    }
}