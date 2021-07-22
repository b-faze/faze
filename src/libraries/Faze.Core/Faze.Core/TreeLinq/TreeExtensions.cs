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
        #region IsLeaf

        public static bool IsLeaf<TValue>(this Tree<TValue> tree)
        {
            return tree.Children == null || !tree.Children.Any() || tree.Children.All(child => child == null);
        }

        #endregion IsLeaf

        #region GetLeaves

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

        #endregion GetLeaves

        #region LimitDepth

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

        #endregion LimitDepth

        #region SelectDepthFirst

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

        #endregion SelectDepthFirst

        #region SelectBreadthFirst
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

        #endregion SelectBreadthFirst

        #region MapValue

        /// <summary>
        /// Creates a new Tree with values mapped using the provided function
        /// </summary>
        public static Tree<TOutValue> MapValue<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn)
        {
            return MapValue(tree, (value, info) => fn(value));
        }

        public static Tree<TOutValue> MapValue<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn)
        {
            if (tree == null)
                return null;

            var info = TreeMapInfo.Root();
            return MapValueHelper(tree, fn, info);
        }

        public static Tree<Color> MapValue(this Tree<double> tree, IColorInterpolator colorInterpolator)
        {
            return tree.MapValue(colorInterpolator.GetColor);
        }

        private static Tree<TOutValue> MapValueHelper<TInValue, TOutValue>(Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn, TreeMapInfo info)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree.Value, info);
            var newChildren = tree.Children
                ?.Select((c, i) => MapValueHelper(c, fn, info.Child(i)));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        #endregion MapValue

        #region MapValueAgg

        /// <summary>
        /// Applies a function to the leaves of a tree and propagates the results up to the root
        /// </summary>
        /// <typeparam name="TInValue"></typeparam>
        /// <typeparam name="TOutValue"></typeparam>
        /// <param name="tree"></param>
        /// <param name="fn"></param>
        /// <param name="aggFactory"></param>
        /// <returns></returns>
        public static Tree<TOutValue> MapValueAgg<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TOutValue> fn, Func<TOutValue> aggFactory)
            where TOutValue : IResultAggregate<TOutValue>
        {
            return MapValueAgg(tree, (value, info) => fn(value), aggFactory);
        }

        public static Tree<TOutValue> MapValueAgg<TInValue, TOutValue>(this Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn, Func<TOutValue> aggFactory)
            where TOutValue : IResultAggregate<TOutValue>
        {
            return MapValueAggHelper(tree, fn, aggFactory, TreeMapInfo.Root());
        }

        private static Tree<TOutValue> MapValueAggHelper<TInValue, TOutValue>(Tree<TInValue> tree, Func<TInValue, TreeMapInfo, TOutValue> fn, Func<TOutValue> aggFactory, TreeMapInfo info)
            where TOutValue : IResultAggregate<TOutValue>
        {
            if (tree == null)
                return null;

            if (tree.IsLeaf())
            {
                return new Tree<TOutValue>(fn(tree.Value, info));
            }

            var children = tree.Children
                    .Select((child, i) => MapValueAggHelper(child, fn, aggFactory, info.Child(i)))
                    .ToList();

            var agg = aggFactory();
            foreach (var childValue in children.Where(x => x != null).Select(x => x.Value))
            {
                agg.Add(childValue);
            }

            return new Tree<TOutValue>(agg, children);
        }

        #endregion MapValueAgg

        #region MapTree

        public static Tree<TOutValue> MapTree<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TOutValue> fn)
        {
            return MapTree(tree, (value, info) => fn(value));
        }

        public static Tree<TOutValue> MapTree<TInValue, TOutValue>(this Tree<TInValue> tree, Func<Tree<TInValue>, TreeMapInfo, TOutValue> fn)
        {
            return MapTreeHelper(tree, fn, TreeMapInfo.Root());
        }

        private static Tree<TOutValue> MapTreeHelper<TInValue, TOutValue>(Tree<TInValue> tree, Func<Tree<TInValue>, TreeMapInfo, TOutValue> fn, TreeMapInfo info)
        {
            if (tree == null)
                return null;

            var newValue = fn(tree, info);
            var newChildren = tree.Children?.Select((c, i) => MapTreeHelper(c, fn, info.Child(i)));

            return new Tree<TOutValue>(newValue, newChildren);
        }

        #endregion MapTree

        #region Normalise

        public static Tree<double> Normalise(this Tree<double> tree)
        {
            var maxValue = tree.SelectDepthFirst().Max();
            var minValue = tree.SelectDepthFirst().Min();

            return tree.MapValue(v => (v - minValue) / (maxValue - minValue));
        }

        public static Tree<double> NormaliseSiblings(this Tree<double> tree)
        {
            return NormaliseSiblingsHelper(tree, tree.Value, tree.Value);
        }

        public static Tree<double> NormaliseSiblingsHelper(this Tree<double> tree, double minValue, double maxValue)
        {
            var value = maxValue > minValue ? (tree.Value - minValue) / (maxValue - minValue) : 0;

            if (tree.IsLeaf())
                return new Tree<double>(value);

            var childMaxValue = tree.Children.Select(child => child.Value).Max();
            var childMinValue = tree.Children.Select(child => child.Value).Min();

            return new Tree<double>(value, tree.Children?.Select(child => NormaliseSiblingsHelper(child, childMinValue, childMaxValue)));
        }


        #endregion Normalise
    }
}
