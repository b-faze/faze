using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;

namespace Faze.Rendering.TreeLinq
{
    public static class TreeExtensions
    {
        /// <summary>
        /// Creates a new Tree with values mapped using the provided function
        /// </summary>
        public static Tree<TOutValue> Map<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn)
        {
            var newValue = fn(tree.Value);
            var newChildren = tree.Children.Select(c => Map(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<IGameState<TMove, TResult, TPlayer>> ToStateTree<TMove, TResult, TPlayer>(this IGameState<TMove, TResult, TPlayer> state)
        {
            var children = state.AvailableMoves.Select(move =>
            {
                var newState = state.Move(move);
                return ToStateTree(newState);
            });

            return new Tree<IGameState<TMove, TResult, TPlayer>>(state, children);
        }

        public static Tree<TMove[]> ToPathTree<TMove, TResult, TPlayer>(this IGameState<TMove, TResult, TPlayer> state)
        {
            return ToPathTreeHelper(state, new TMove[0]);
        }

        private static Tree<TMove[]> ToPathTreeHelper<TMove, TResult, TPlayer>(this IGameState<TMove, TResult, TPlayer> state, TMove[] path)
        {
            var children = state.AvailableMoves.Select(move =>
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
