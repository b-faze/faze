using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsPlaceOrBetState : SkullsState
    {
        public SkullsPlaceOrBetState(SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
            : base(playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<ISkullsMove, SkullsResult> Move(ISkullsMove move)
        {
            if (move is SkullsPlacementMove placementMove)
            {
                var newEnvironment = playerEnvironments.Place(currentPlayerIndex, placementMove.Token);
                var newPlayerIndex = newEnvironment.GetNextPlayerIndex(currentPlayerIndex);

                return new SkullsPlaceOrBetState(newEnvironment, newPlayerIndex);
            }

            if (move is SkullsBetMove betMove)
            {
                var newEnvironment = playerEnvironments.Bet(currentPlayerIndex, betMove);
                var newPlayerIndex = newEnvironment.GetNextPlayerIndex(currentPlayerIndex);

                if (newEnvironment.GetMaxPossibleBet() == betMove.Bet)
                {
                    return new SkullsRevealState(newEnvironment, currentPlayerIndex, betMove.Bet.Value);
                }

                return new SkullsBetState(newEnvironment, newPlayerIndex);
            }

            throw new Exception("Invalid move");
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            var moves = new List<ISkullsMove>();

            moves.AddRange(playerEnvironments.GetForPlayer(currentPlayerIndex).GetPlacementMoves()
                .Select(x => (ISkullsMove)x));
            moves.AddRange(GetBetMoves()
                .Select(x => (ISkullsMove)x));

            return moves.ToArray();
        }

        private IEnumerable<SkullsBetMove> GetBetMoves()
        {
            return Enumerable.Range(1, playerEnvironments.GetMaxPossibleBet())
                .Select(x => new SkullsBetMove(x));
        }
    }
}
