using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using System.Linq;

namespace Faze.Core.Extensions
{
    public static class GameStateTreeExtensions
    {
        public static Tree<IGameState<TMove, TResult>> ToStateTree<TMove, TResult>(this IGameState<TMove, TResult> state)
        {
            if (state == null)
                return null;

            var children = state.GetAvailableMoves().Select(move =>
            {
                var newState = state.Move(move);
                return ToStateTree(newState);
            });

            return new Tree<IGameState<TMove, TResult>>(state, children);
        }

        public static Tree<IGameState<TMove, TResult>> ToStateTree<TMove, TResult>(this IGameState<TMove, TResult> state, IGameStateTreeAdapter<TMove> adapter)
        {
            if (state == null)
                return null;

            var children = adapter.GetChildren(state)?.Select(childState => ToStateTree(childState, adapter));

            return new Tree<IGameState<TMove, TResult>>(state, children);
        }

        public static Tree<TMove[]> ToPathTree<TMove, TResult>(this IGameState<TMove, TResult> state)
        {
            return ToPathTreeHelper(state, new TMove[0]);
        }

        private static Tree<TMove[]> ToPathTreeHelper<TMove, TResult>(this IGameState<TMove, TResult> state, TMove[] path)
        {
            var children = state.GetAvailableMoves().Select(move =>
            {
                var newState = state.Move(move);

                var newPath = new TMove[path.Length + 1];
                path.CopyTo(newPath, 0);
                newPath[path.Length] = move;

                return ToPathTreeHelper(newState, newPath);
            });

            return new Tree<TMove[]>(path, children);
        }
    }
}
