namespace Faze.Games.Skulls
{
    public struct SkullsRevealMove<TPlayer> : ISkullsMove
    {
        public SkullsRevealMove(TPlayer targetPlayer)
        {
            TargetPlayer = targetPlayer;
        }

        public TPlayer TargetPlayer { get; set; }
    }
}
