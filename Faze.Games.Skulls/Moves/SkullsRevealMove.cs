namespace Faze.Instances.Games.Skulls
{
    public struct SkullsRevealMove : ISkullsMove
    {
        public SkullsRevealMove(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public int PlayerIndex { get; set; }
    }
}
