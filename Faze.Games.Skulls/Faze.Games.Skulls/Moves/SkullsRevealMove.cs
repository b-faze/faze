namespace Faze.Instances.Games.Skulls
{
    public class SkullsRevealMove : SkullsMove
    {
        public SkullsRevealMove(int playerIndex)
        {
            PlayerIndex = playerIndex;
        }

        public int PlayerIndex { get; set; }
    }
}
