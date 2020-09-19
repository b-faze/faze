using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    internal class SkullsResultState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsResultState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex,
            WinLoseResult<TPlayer> result)
            : base(players, playerEnvironments, currentPlayerIndex)
        {
            Result = result;
        }

        public override IGameState<ISkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            throw new Exception("Invalid move. Game is already over");
        }

        protected override ISkullsMove[] GetAvailableMoves()
        {
            return new ISkullsMove[0];
        }
    }
}
