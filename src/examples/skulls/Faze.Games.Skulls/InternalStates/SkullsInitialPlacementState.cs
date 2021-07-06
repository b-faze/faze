using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsInitialPlacementState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsInitialPlacementState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex)
            : base(playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            if (!(move is SkullsPlacementMove placementMove))
                throw new Exception("The provided SkullMove is not supported");

            var newPlayerEnvironments = playerEnvironments.Place(currentPlayerIndex, placementMove.Token);
            var newPlayerIndex = newPlayerEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            // if we've come back around to the first player then move into the next phase
            if (newPlayerIndex == 0) 
            {
                return new SkullsPlaceOrBetState<TPlayer>(newPlayerEnvironments, newPlayerIndex);
            }

            return new SkullsInitialPlacementState<TPlayer>(newPlayerEnvironments, newPlayerIndex);
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            return playerEnvironments
                .GetForPlayer(currentPlayerIndex)
                .GetPlacementMoves()
                .Select(x => (ISkullsMove)x)
                .ToArray();
        }
    }
}
