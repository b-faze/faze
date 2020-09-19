namespace Faze.Instances.Games.Skulls
{
    public class SkullsPlacementMove : SkullsMove
    {
        public SkullsPlacementMove(SkullsTokenType token)
        {
            Token = token;
        }

        public SkullsTokenType Token { get; }
    }
}
