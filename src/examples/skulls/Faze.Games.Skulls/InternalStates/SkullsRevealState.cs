using Faze.Abstractions.GameStates;
using System;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsRevealState<TPlayer> : SkullsState<TPlayer>
    {
        private readonly int targetBet;

        public SkullsRevealState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex, int targetBet)
            : base(playerEnvironments, currentPlayerIndex)
        {
            this.targetBet = targetBet;
        }

        public override IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            if (!(move is SkullsRevealMove<TPlayer> revealMove))
                throw new Exception("Only reveal moves allowed during reveal phase");

            var revealTargetPlayer = revealMove.TargetPlayer;
            var newPlayerEnvironments = playerEnvironments.Reveal(revealTargetPlayer);
            var revealTargetPlayerIndex = playerEnvironments.GetPlayerIndex(revealTargetPlayer);

            if (newPlayerEnvironments.IsSkullRevealed())
                return new SkullsRevealPenaltyState<TPlayer>(playerEnvironments, currentPlayerIndex, revealTargetPlayerIndex);

            if (newPlayerEnvironments.GetTotalRevealed() == targetBet)
            {
                // if the player already has a win - they win the game!
                if (newPlayerEnvironments.GetForPlayer(currentPlayerIndex).HasWin) 
                {
                    var result = new SkullsResult<TPlayer>(CurrentPlayer);
                    return new SkullsResultState<TPlayer>(newPlayerEnvironments, currentPlayerIndex, result);
                }

                var nextEnvironments = newPlayerEnvironments.MarkWinning(currentPlayerIndex);
                var newPlayerIndex = nextEnvironments.GetNextPlayerIndex(currentPlayerIndex);
                return new SkullsPlaceOrBetState<TPlayer>(nextEnvironments, newPlayerIndex);
            }

            return new SkullsRevealState<TPlayer>(newPlayerEnvironments, currentPlayerIndex, targetBet);
        }

        protected override ISkullsMove[] GetAvailableMoves()
        {
            if (playerEnvironments.GetForPlayer(currentPlayerIndex).CanReveal())
                return new ISkullsMove[] { new SkullsRevealMove<TPlayer>(CurrentPlayer) };

            return playerEnvironments.GetRevealMoves()
                .ToArray();
        }
    }


}
