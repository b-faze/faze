namespace Faze.Abstractions.Players
{
    /// <summary>
    /// Uniquely identifies a player
    /// </summary>
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

        public override bool Equals(object obj)
        {
            if (obj is PlayerIndex playerIndex)
                return index.Equals(playerIndex.index);

            return index.Equals(obj);
        }

        public override int GetHashCode()
        {
            return index.GetHashCode();
        }

        public override string ToString()
        {
            return index.ToString();
        }
    }
}