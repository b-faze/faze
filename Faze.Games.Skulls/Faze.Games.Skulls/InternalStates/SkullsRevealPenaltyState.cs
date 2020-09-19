using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    internal class SkullsRevealPenaltyState<TPlayer> : SkullsState<TPlayer>
    {
        private readonly int playerIndexWithPenalty;

        public SkullsRevealPenaltyState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex, int skullPlayer)
            : base(players, playerEnvironments, skullPlayer)
        {
            this.playerIndexWithPenalty = currentPlayerIndex;
        }

        public override IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(SkullsMove move)
        {
            if (!(move is SkullsPenaltyDiscardMove discardMove))
                throw new Exception("Only discard moves allowed");

            var newEnvironments = playerEnvironments.Discard(playerIndexWithPenalty, discardMove.HandIndex);
            var newPlayerIndex = newEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            return new SkullsPlaceOrBetState<TPlayer>(players, newEnvironments, newPlayerIndex);
        }

        protected override SkullsMove[] GetAvailableMoves()
        {
            return playerEnvironments
                .GetForPlayer(playerIndexWithPenalty)
                .GetPenaltyDiscardtMoves()
                .ToArray();
        }
    }


}
