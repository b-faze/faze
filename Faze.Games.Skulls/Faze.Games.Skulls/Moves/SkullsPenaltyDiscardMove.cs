namespace Faze.Instances.Games.Skulls
{
    public class SkullsPenaltyDiscardMove : SkullsMove
    {
        public SkullsPenaltyDiscardMove(int handIndex)
        {
            HandIndex = handIndex;
        }

        public int HandIndex { get; private set; }
    }
}
