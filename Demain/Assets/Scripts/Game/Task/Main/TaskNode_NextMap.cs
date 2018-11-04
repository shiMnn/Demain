namespace game {
    public class TaskNode_NextMap : TaskNodeBase {
        public override bool Setup() {
            return true;
        }
        public override bool Exec() {
            // マップを全て破棄
            BlockManager.Instance.AllRelease();
            return true;
        }
        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_CreateMap());
            return true;
        }
    }
}