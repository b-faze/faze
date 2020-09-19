using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    internal class SkullsRevealState<TPlayer> : SkullsState<TPlayer>
    {
        private readonly int targetBet;

        public SkullsRevealState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex, int targetBet)
            : base(players, playerEnvironments, currentPlayerIndex)
        {
            this.targetBet = targetBet;
        }

        public override IGameState<ISkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            if (!(move is SkullsRevealMove revealMove))
                throw new Exception("Only reveal moves allowed during reveal phase");

            var revealPlayerIndex = revealMove.PlayerIndex;
            var newPlayerEnvironments = playerEnvironments.Reveal(revealPlayerIndex);

            if (newPlayerEnvironments.IsSkullRevealed())
                return new SkullsRevealPenaltyState<TPlayer>(players, playerEnvironments, currentPlayerIndex, revealPlayerIndex);

            if (newPlayerEnvironments.GetTotalRevealed() == targetBet)
            {
                // if the player already has a win - they win the game!
                if (newPlayerEnvironments.GetForPlayer(currentPlayerIndex).HasWin) 
                {
                    var result = WinLoseResult<TPlayer>.Win(CurrentPlayer);
                    return new SkullsResultState<TPlayer>(players, newPlayerEnvironments, currentPlayerIndex, result);
                }

                var nextEnvironments = newPlayerEnvironments.MarkWinning(currentPlayerIndex);
                var newPlayerIndex = nextEnvironments.GetNextPlayerIndex(currentPlayerIndex);
                return new SkullsPlaceOrBetState<TPlayer>(players, nextEnvironments, newPlayerIndex);
            }

            return new SkullsRevealState<TPlayer>(players, newPlayerEnvironments, currentPlayerIndex, targetBet);
        }

        protected override ISkullsMove[] GetAvailableMoves()
        {
            if (playerEnvironments.GetForPlayer(currentPlayerIndex).CanReveal())
                return new ISkullsMove[] { new SkullsRevealMove(currentPlayerIndex) };

            return playerEnvironments.GetRevealMoves()
                .ToArray();
        }
    }


}
