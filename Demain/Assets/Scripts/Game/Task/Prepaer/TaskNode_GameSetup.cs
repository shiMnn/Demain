using UnityEngine;

namespace game {
    public class TaskNode_GameSetup : TaskNodeBase{
        public override bool Setup() {
            return true;
        }
        public override bool Exec() {
            return true;
        }
        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_CreateMap());
            return true;
        }
    }
}