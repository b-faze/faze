using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    internal class SkullsPlaceOrBetState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsPlaceOrBetState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
            : base(players, playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(SkullsMove move)
        {
            if (move is SkullsPlacementMove placementMove)
            {
                var newEnvironment = playerEnvironments.Place(currentPlayerIndex, placementMove.Token);
                var newPlayerIndex = newEnvironment.GetNextPlayerIndex(currentPlayerIndex);

                return new SkullsPlaceOrBetState<TPlayer>(players, newEnvironment, newPlayerIndex);
            }

            if (move is SkullsBetMove betMove)
            {
                var newEnvironment = playerEnvironments.Bet(currentPlayerIndex, betMove);
                var newPlayerIndex = newEnvironment.GetNextPlayerIndex(currentPlayerIndex);

                if (newEnvironment.GetMaxPossibleBet() == betMove.Bet)
                {
                    return new SkullsRevealState<TPlayer>(players, newEnvironment, currentPlayerIndex, betMove.Bet.Value);
                }

                return new SkullsBetState<TPlayer>(players, newEnvironment, newPlayerIndex);
            }

            throw new Exception("Invalid move");
        }

        protected override SkullsMove[] GetAvailableMoves()
        {
            var moves = new List<SkullsMove>();

            moves.AddRange(playerEnvironments.GetForPlayer(currentPlayerIndex).GetPlacementMoves());
            moves.AddRange(GetBetMoves());

            return moves.ToArray();
        }

        private IEnumerable<SkullsMove> GetBetMoves()
        {
            return Enumerable.Range(1, playerEnvironments.GetMaxPossibleBet())
                .Select(x => new SkullsBetMove(x));
        }
    }
}
