namespace Faze.Instances.Games.Skulls
{
    public struct SkullsPenaltyDiscardMove : ISkullsMove
    {
        public SkullsPenaltyDiscardMove(int handIndex)
        {
            HandIndex = handIndex;
        }

        public int HandIndex { get; private set; }
    }
}
