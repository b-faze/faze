using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Linq;

namespace Faze.Games.Skulls
{
    internal class SkullsRevealPenaltyState<TPlayer> : SkullsState<TPlayer>
    {
        private readonly int playerIndexWithPenalty;

        public SkullsRevealPenaltyState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex, int skullPlayerIndex)
            : base(playerEnvironments, skullPlayerIndex)
        {
            this.playerIndexWithPenalty = currentPlayerIndex;
        }

        public override IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            if (!(move is SkullsPenaltyDiscardMove discardMove))
                throw new Exception("Only discard moves allowed");

            var newEnvironments = playerEnvironments.Discard(playerIndexWithPenalty, discardMove.HandIndex);
            var newPlayerIndex = newEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            return new SkullsPlaceOrBetState<TPlayer>(newEnvironments, newPlayerIndex);
        }

        protected override ISkullsMove[] GetAvailableMoves()
        {
            return playerEnvironments
                .GetForPlayer(playerIndexWithPenalty)
                .GetPenaltyDiscardMoves()
                .Select(x => (ISkullsMove)x)
                .ToArray();
        }
    }


}
