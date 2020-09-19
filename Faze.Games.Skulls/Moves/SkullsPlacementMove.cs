namespace Faze.Games.Skulls
{
    public struct SkullsPlacementMove : ISkullsMove
    {
        public SkullsPlacementMove(SkullsTokenType token)
        {
            Token = token;
        }

        public SkullsTokenType Token { get; }
    }
}
