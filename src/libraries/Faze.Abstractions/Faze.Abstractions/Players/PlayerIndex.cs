namespace Faze.Abstractions.Players
{
    public struct PlayerIndex
    {
        public static readonly PlayerIndex P1 = 0;
        public static readonly PlayerIndex P2 = 1;

        private int index;

        public PlayerIndex(int index)
        {
            this.index = index;
        }

        public static implicit operator int(PlayerIndex playerIndex)
        {
            return playerIndex.index;
        }

        public static implicit operator PlayerIndex(int index)
        {
            return new PlayerIndex(index);
        }
    }
}