using Faze.Abstractions.Players;

namespace Faze.Games.Skulls
{
    public struct SkullsRevealMove : ISkullsMove
    {
        public SkullsRevealMove(PlayerIndex targetPlayer)
        {
            TargetPlayer = targetPlayer;
        }

        public PlayerIndex TargetPlayer { get; set; }
    }
}
