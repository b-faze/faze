using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsRevealPenaltyState : SkullsState
    {
        private readonly int playerIndexWithPenalty;

        public SkullsRevealPenaltyState(SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex, int skullPlayerIndex)
            : base(playerEnvironments, skullPlayerIndex)
        {
            this.playerIndexWithPenalty = currentPlayerIndex;
        }

        public override IGameState<ISkullsMove, SkullsResult> Move(ISkullsMove move)
        {
            if (!(move is SkullsPenaltyDiscardMove discardMove))
                throw new Exception("Only discard moves allowed");

            var newEnvironments = playerEnvironments.Discard(playerIndexWithPenalty, discardMove.HandIndex);

            if (newEnvironments.OnlyOnePlayerRemaining()) 
            {
                var result = new SkullsResult(CurrentPlayerIndex);
                return new SkullsResultState(newEnvironments, currentPlayerIndex, result);
            }

            // Player who had the skull starts the next round.
            return new SkullsInitialPlacementState(newEnvironments, currentPlayerIndex);
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            return playerEnvironments
                .GetForPlayer(playerIndexWithPenalty)
                .GetPenaltyDiscardMoves()
                .Select(x => (ISkullsMove)x)
                .ToArray();
        }
    }


}
