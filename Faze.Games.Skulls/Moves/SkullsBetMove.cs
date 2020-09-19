namespace Faze.Instances.Games.Skulls
{
    public struct SkullsBetMove : ISkullsMove
    {
        public SkullsBetMove(int? bet)
        {
            Bet = bet;
        }

        public static SkullsBetMove Skip()
        {
            return new SkullsBetMove(null);
        }

        public int? Bet { get; private set; }
        public bool Skipped => !Bet.HasValue;
    }
}
