namespace game {
    public abstract class TaskNodeBase {
        public abstract bool Setup();
        public abstract bool Exec();
        public abstract bool Finish();
    }
}