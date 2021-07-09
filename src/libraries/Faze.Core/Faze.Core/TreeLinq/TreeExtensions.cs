using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;

namespace Faze.Core.TreeLinq
{

    public static class TreeExtensions
    {
        public static bool IsLeaf<TValue>(this Tree<TValue> tree)
        {
            return tree.Children == null || !tree.Children.Any() || tree.Children.All(child => child == null);
        }

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
        public static Tree<TOutValue> MapValue<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree.Value);
            var newChildren = tree.Children?.Select(c => MapValue(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<TOutValue> MapValue<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var info = TreeMapInfo.Root();
            return MapValueHelper(tree, fn, info);
        }

        public static Tree<TOutValue> MapTree<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree);
            var newChildren = tree.Children?.Select(c => MapTree(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<TOutValue> MapTreeWithNullNodes<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TOutValue> fn)
        {
            var newValue = fn(tree);
            var newChildren = tree?.Children?.Select(c => MapTreeWithNullNodes(c, fn));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<TOutValue> MapTreeWithNullNodes<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TreeMapInfo, TOutValue> fn)
        {
            return MapTreeWithNullNodesHelper(tree, fn, TreeMapInfo.Root());
        }

        private static Tree<TOutValue> MapTreeWithNullNodesHelper<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TreeMapInfo, TOutValue> fn, TreeMapInfo info)
        {
            var newValue = fn(tree, info);
            var newChildren = tree?.Children?.Select((c, i) => MapTreeWithNullNodesHelper(c, fn, info.Child(i)));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<TOutValue> MapTreeAgg<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn)
            where TOutValue : IResultAggregate<TOutValue>
        {
            if (tree == null)
                return null;

            if (tree.IsLeaf())
            {
                return new Tree<TOutValue>(fn(tree.Value));
            }

            var children = tree.Children
                    .Select(x => MapTreeAgg(x, fn));

            var value = children
                .Where(x => x != null)
                .Select(x => x.Value)
                .Aggregate((a, b) =>
            {
                a.Value.Add(b.Value);
                return a.Value;
            });

            return new Tree<TOutValue>(value, children);
        }

        private static Tree<TOutValue> MapValueHelper<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn, TreeMapInfo info)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree.Value, info);
            var newChildren = tree.Children
                ?.Select((c, i) => MapValueHelper(c, fn, info.Child(i)));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        public static Tree<Color> MapValue(this Tree<double> tree, IColorInterpolator colorInterpolator)
        {
            return tree.MapValue(colorInterpolator.GetColor);
        }

        public static Tree<TValue> LimitDepth<TValue>(this Tree<TValue> tree, int depth)
        {
            return LimitDepthHelper(tree, depth, TreeMapInfo.Root());
        }

        private static Tree<TValue> LimitDepthHelper<TValue>(this Tree<TValue> tree, int depth, TreeMapInfo info)
        {
            if (tree == null)
                return null;

            if (info.Depth >= depth)
            {
                return new Tree<TValue>(tree.Value, new Tree<TValue>[0]);
            }

            var children = tree.Children?.Select((c, i) => LimitDepthHelper(c, depth, info.Child(i)));

            return new Tree<TValue>(tree.Value, children);
        }

        public static IEnumerable<Tree<TValue>> GetLeaves<TValue>(this Tree<TValue> tree)
        {
            if (tree.IsLeaf())
            {
                yield return tree;
                yield break;
            }

            var children = tree.Children;

            foreach (var child in children)
            {
                if (child == null)
                    continue;

                foreach (var leaf in GetLeaves(child))
                    yield return leaf;
            }
        }
    }
}
