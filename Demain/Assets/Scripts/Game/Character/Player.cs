namespace game {
    public class Player : CharacterBase{
        public override void Initialize(int x, int y, int z) {
            base.Initialize(x, y, z);
            Controlling = true;
        }
    }
}