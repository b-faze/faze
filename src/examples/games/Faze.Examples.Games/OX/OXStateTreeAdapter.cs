using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using System.Collections.Generic;

namespace Faze.Examples.Games.OX
{
    public class OXStateTreeAdapter : IGameStateTreeAdapter<GridMove>
    {
        public IEnumerable<IGameState<GridMove, TResult>> GetChildren<TResult>(IGameState<GridMove, TResult> state)
        {
            var result = new IGameState<GridMove, TResult>[9];

            foreach (var move in state.GetAvailableMoves())
            {
                result[move] = state.Move(move);
            }

            return result;
        }
    }

}
