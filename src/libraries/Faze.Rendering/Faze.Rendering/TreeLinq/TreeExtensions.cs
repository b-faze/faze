using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;

namespace Faze.Rendering.TreeLinq
{

    public static class TreeExtensions
    {
        public static IEnumerable<TValue> SelectDepthFirst<TValue>(this Tree<TValue> tree)
        {
            yield return tree.Value;

            if (tree.Children == null)
                yield break;

            foreach (var child in tree.Children)
            {
                foreach (var value in child.SelectDepthFirst())
                {
                    yield return value;
                }
            }
        }

        public static IEnumerable<TValue> SelectBreadthFirst<TValue>(this Tree<TValue> tree)
        {
            var queue = new Queue<Tree<TValue>>();
            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;

                if (node.Children != null)
                {
                    foreach (var child in node.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new Tree with values mapped using the provided function
        /// </summary>
        public static Tree<TOutValue> Map<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree.Value);
            var newChildren = tree.Children?.Select(c => Map(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<TOutValue> Map<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var info = new TreeMapInfo(0, 0);
            return MapHelper(tree, fn, info);
        }

        public static Tree<TOutValue> MapTree<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree);
            var newChildren = tree.Children?.Select(c => MapTree(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        private static Tree<TOutValue> MapHelper<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn, TreeMapInfo info)
        {
            var newValue = fn(tree.Value, info);
            var newChildren = tree.Children
                ?.Select((c, i) => MapHelper(c, fn, new TreeMapInfo(info.Depth + 1, i)));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<Color> Map(this Tree<double> tree, IColorInterpolator colorInterpolator)
        {
            return tree.Map(colorInterpolator.GetColor);
        }

        public static Tree<TValue> LimitDepth<TValue>(this Tree<TValue> tree, int depth)
        {
            return tree;
        }

        private static Tree<TValue> LimitDepthHelper<TValue>(this Tree<TValue> tree, int depth, TreeMapInfo info)
        {
            if (info.Depth >= depth)
            {
                return new Tree<TValue>(tree.Value, new Tree<TValue>[0]);
            }

            var children = tree.Children?.Select((c, i) => LimitDepthHelper(c, depth, new TreeMapInfo(info.Depth + 1, i)));

            return new Tree<TValue>(tree.Value, children);
        }

        public static IEnumerable<Tree<TValue>> GetLeaves<TValue>(this Tree<TValue> tree)
        {
            var children = tree.Children;
            if (children == null || !children.Any())
            {
                yield return tree;
                yield break;
            }


            foreach (var child in children)
            {
                foreach (var leaf in GetLeaves(child))
                    yield return leaf;
            }
        }

        public static Tree<IGameState<TMove, TResult>> ToStateTree<TMove, TResult>(this IGameState<TMove, TResult> state)
        {
            var children = state.GetAvailableMoves().Select(move =>
            {
                var newState = state.Move(move);
                return ToStateTree(newState);
            });

            return new Tree<IGameState<TMove, TResult>>(state, children);
        }

        public static Tree<IGameState<TMove, TResult>> ToStateTree<TMove, TResult>(this IGameState<TMove, TResult> state, Func<TMove, int> moveIndexer, int totalChildren)
        {
            var children = new Tree<IGameState<TMove, TResult>>[totalChildren];
            foreach (var move in state.GetAvailableMoves()) 
            {
                children[moveIndexer(move)] = ToStateTree(state.Move(move), moveIndexer, totalChildren);
            }

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
