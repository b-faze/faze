using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class PathIterator<TMove, TResult> : IIterater<IGameState<TMove, TResult>, IGameState<TMove, TResult>>
    {
        private readonly TMove[] path;

        public PathIterator(TMove[] path)
        {
            this.path = path;
        }

        public IEnumerable<IGameState<TMove, TResult>> GetEnumerable(IGameState<TMove, TResult> input, IProgressTracker progress = null)
        {
            for (var n = 1; n <= path.Length; n++)
            {
                var newState = input;
                foreach (var move in path.Take(n))
                {
                    newState = newState.Move(move);
                }

                yield return newState;
            }
        }
    }
}
