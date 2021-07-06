using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;

namespace Faze.Games.Skulls
{
    internal class SkullsResultState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsResultState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex,
            SkullsResult<TPlayer> result)
            : base(playerEnvironments, currentPlayerIndex)
        {
            Result = result;
        }

        public override IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            throw new Exception("Invalid move. Game is already over");
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            return new ISkullsMove[0];
        }
    }
}
