using Faze.Abstractions.GameStates;
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

            if (newEnvironments.OnlyOnePlayerRemaining()) 
            {
                var result = new SkullsResult<TPlayer>(CurrentPlayer);
                return new SkullsResultState<TPlayer>(newEnvironments, currentPlayerIndex, result);
            }

            // Player who had the skull starts the next round.
            return new SkullsInitialPlacementState<TPlayer>(newEnvironments, currentPlayerIndex);
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
