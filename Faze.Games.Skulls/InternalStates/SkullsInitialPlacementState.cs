using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    internal class SkullsInitialPlacementState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsInitialPlacementState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
            : base(players, playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(SkullsMove move)
        {
            if (!(move is SkullsPlacementMove placementMove))
                throw new Exception("The provided SkullMove is not supported");

            var newPlayerEnvironments = playerEnvironments.Place(currentPlayerIndex, placementMove.Token);
            var newPlayerIndex = newPlayerEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            // if we've come back around to the first player then move into the next phase
            if (newPlayerIndex == 0) 
            {
                return new SkullsPlaceOrBetState<TPlayer>(players, newPlayerEnvironments, newPlayerIndex);
            }

            return new SkullsInitialPlacementState<TPlayer>(players, newPlayerEnvironments, newPlayerIndex);
        }

        protected override SkullsMove[] GetAvailableMoves()
        {
            return playerEnvironments.GetForPlayer(currentPlayerIndex).GetPlacementMoves().ToArray();
        }
    }
}
