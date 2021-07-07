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
        /// <param name="availableMoves"></param>
        /// <returns></returns>
        IEnumerable<IGameState<TMove, TResult>> GetChildren<TResult>(IGameState<TMove, TResult> state);
    }
}
