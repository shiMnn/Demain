namespace game {
    public class Player : CharacterBase{
        public override void Initialize(int x, int y) {
            base.Initialize(x, y);
            Controlling = true;
        }
    }
}