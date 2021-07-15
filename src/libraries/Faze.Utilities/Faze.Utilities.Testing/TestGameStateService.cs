using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Utilities.Testing
{
    public class TestGameStateService
    {
        public IGameState<TMove, TResult> CreateState<TMove, TResult>(IEnumerable<TMove> availableMoves)
        {
            return new TestGameState<TMove, TResult>
            {
                AvailableMoves = availableMoves
            };
        }

        public IGameState<int, TResult> CreateState<TResult>(Tree<TResult> gameTree)
        {
            return new TestTreeGameState<TResult>(gameTree);
        }
    }
}
