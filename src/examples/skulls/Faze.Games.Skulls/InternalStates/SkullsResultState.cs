using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;

namespace Faze.Games.Skulls
{
    internal class SkullsResultState : SkullsState
    {
        public SkullsResultState(SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex,
            SkullsResult result)
            : base(playerEnvironments, currentPlayerIndex)
        {
            Result = result;
        }

        public override IGameState<ISkullsMove, SkullsResult> Move(ISkullsMove move)
        {
            throw new Exception("Invalid move. Game is already over");
        }

        public override IEnumerable<ISkullsMove> GetAvailableMoves()
        {
            return new ISkullsMove[0];
        }
    }
}
