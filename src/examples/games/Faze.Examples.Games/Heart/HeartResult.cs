namespace Faze.Examples.Games.Heart
{
    public struct HeartResult
    {
        public int P1Points { get; set; }
        public int P2Points { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is HeartResult result)
            {
                return result.P1Points == P1Points && result.P2Points == P2Points;
            }

            return false;
        }
    }
}
