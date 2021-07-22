using Faze.Abstractions.GameStates;
using System.Collections.Generic;

namespace Faze.Abstractions.Core
{
    public interface IGameStateTreeAdapter<TMove>
    {
        /// <summary>
        /// Allows handling creating placeholders for moves that are no longer available
        /// This will allow the state tree to have a consistent structure
        /// </summary>
        /// <param name="state">Game state</param>
        /// <returns>New game states for each move</returns>
        IEnumerable<IGameState<TMove, TResult>> GetChildren<TResult>(IGameState<TMove, TResult> state);
    }
}
