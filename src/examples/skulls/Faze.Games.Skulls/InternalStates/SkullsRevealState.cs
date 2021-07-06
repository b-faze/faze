using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsRevealState : SkullsState
    {
        private readonly int targetBet;

        public SkullsRevealState(SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex, int targetBet)
            : base(playerEnvironments, currentPlayerIndex)
        {
            this.targetBet = targetBet;
        }

        public override IGameState<ISkullsMove, SkullsResult> Move(ISkullsMove move)
        {
            if (!(move is SkullsRevealMove revealMove))
                throw new Exception("Only reveal moves allowed during reveal phase");

            var revealTargetPlayer = revealMove.TargetPlayer;
            var newPlayerEnvironments = playerEnvironments.Reveal(revealTargetPlayer);
            var revealTargetPlayerIndex = playerEnvironments.GetPlayerIndex(revealTargetPlayer);

            if (newPlayerEnvironments.IsSkullRevealed())
                return new SkullsRevealPenaltyState(playerEnvironments, currentPlayerIndex, revealTargetPlayerIndex);

            if (newPlayerEnvironments.GetTotalRevealed() == targetBet)
            {
                // if the player already has a win - they win the game!
                if (newPlayerEnvironments.GetForPlayer(currentPlayerIndex).HasWin) 
                {
                    var result = new SkullsResult(CurrentPlayerIndex);
                    return new SkullsResultState(newPlayerEnvironments, currentPlayerIndex, result);
                }

                var nextEnvironments = newPlayerEnvironments.MarkWinning(currentPlayerIndex);
                var newPlayerIndex = nextEnvironments.GetNextPlayerIndex(currentPlayerIndex);
                return new SkullsPlaceOrBetState(nextEnvironments, newPlayerIndex);
            }

            return new SkullsRevealState(newPlayerEnvironments, currentPlayerIndex, targetBet);
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            if (playerEnvironments.GetForPlayer(currentPlayerIndex).CanReveal())
                return new ISkullsMove[] { new SkullsRevealMove(CurrentPlayerIndex) };

            return playerEnvironments.GetRevealMoves()
                .ToArray();
        }
    }


}
