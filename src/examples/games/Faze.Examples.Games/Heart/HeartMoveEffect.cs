namespace Faze.Examples.Games.Heart
{
    public class HeartMoveEffect
    {
        public static HeartMoveEffect Create(int hit, int block, int advantage) => new HeartMoveEffect(hit, block, advantage);
        public static HeartMoveEffect None() => new HeartMoveEffect(0, 0, 0);
        private HeartMoveEffect(int hit, int block, int advantage)
        {
            Hit = hit;
            Block = block;
            Advantage = advantage;
        }

        public int Hit { get; private set; }
        public int Block { get; private set; }
        public int Advantage { get; private set; }
    }
}
