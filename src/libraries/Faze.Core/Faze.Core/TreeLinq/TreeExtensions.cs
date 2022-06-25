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

        #region Where

        public static Tree<TValue> Where<TValue>(this Tree<TValue> tree, Func<Tree<TValue>, TreeMapInfo, bool> predicate)
        {
            return WhereHelper(tree, predicate, TreeMapInfo.Root());
        }

        private static Tree<TValue> WhereHelper<TValue>(Tree<TValue> tree, Func<Tree<TValue>, TreeMapInfo, bool> predicate, TreeMapInfo info)
        {
            if (!predicate(tree, info))
                return null;

            var children = tree.Children?.Select((child, i) => WhereHelper(child, predicate, info.Child(i)));
            return new Tree<TValue>(tree.Value, children);
        }

        #endregion Where

        #region SelectDepthFirst

        public static IEnumerable<TValue> SelectDepthFirst<TValue>(this Tree<TValue> tree)
        {
            yield return tree.Value;

            if (tree.Children == null)
                yield break;

            foreach (var child in tree.Children)
            {
                if (child == null)
                    continue;

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

        #region SelectPath

        public static IEnumerable<TValue> SelectPath<TValue>(this Tree<TValue> tree, IEnumerable<int> path)
        {
            if (tree == null)
                yield break;

            // yield root
            yield return tree.Value;

            foreach (var index in path)
            {
                tree = tree.Children?.ElementAtOrDefault(index);
                if (tree == null)
                    yield break;

                yield return tree.Value;
            }
        }

        #endregion SelectPath

        #region Find

        public static Tree<TValue> Find<TValue>(this Tree<TValue> tree, IEnumerable<int> path)
        {
            foreach (var index in path)
            {
                tree = tree.Children.ElementAt(index);
            }

            return tree;
        }

        public static bool TryFind<TValue>(this Tree<TValue> tree, IEnumerable<int> path, out Tree<TValue> foundTree)
        {
            foreach (var index in path)
            {
                tree = tree?.Children.ElementAtOrDefault(index);
            }

            foundTree = tree;

            return tree != null;
        }

        #endregion Find

        #region WithInfo 
        public static Tree<(TValue value, TreeMapInfo info)> WithInfo<TValue>(this Tree<TValue> tree)
        {
            return MapValue(tree, (value, info) => (value, info));
        }

        #endregion WithInfo

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

        private static Tree<double> NormaliseSiblingsHelper(this Tree<double> tree, double minValue, double maxValue)
        {
            if (tree == null) 
                return null;

            var value = maxValue > minValue ? (tree.Value - minValue) / (maxValue - minValue) : 0.5;

            if (tree.IsLeaf())
                return new Tree<double>(value);

            var childMaxValue = tree.Children.Where(child => child != null).Select(child => child.Value).Max();
            var childMinValue = tree.Children.Where(child => child != null).Select(child => child.Value).Min();

            return new Tree<double>(value, tree.Children?.Select(child => NormaliseSiblingsHelper(child, childMinValue, childMaxValue)));
        }

        public static Tree<double> NormaliseDepth(this Tree<double> tree)
        {
            return NormaliseDepthHelper(tree, tree.Value, tree.Value);
        }

        private static Tree<double> NormaliseDepthHelper(this Tree<double> tree, double minValue, double maxValue)
        {
            if (tree == null)
                return null;

            var value = maxValue > minValue ? (tree.Value - minValue) / (maxValue - minValue) : 0;

            if (tree.IsLeaf())
                return new Tree<double>(value);

            var childMaxValue = tree.Children.Where(child => child != null).Select(child => child.Value).Max();
            var childMinValue = tree.Children.Where(child => child != null).Select(child => child.Value).Min();

            return new Tree<double>(value, tree.Children?.Select(child => NormaliseSiblingsHelper(child, childMinValue, childMaxValue)));
        }


        #endregion Normalise

        public static Tree<T> Evaluate<T>(this Tree<T> tree)
        {
            if (tree?.Children == null)
                return tree;

            return new Tree<T>(tree.Value, tree.Children.Select(c => Evaluate(c)).ToList());
        }

        public static Tree<T> Evaluate<T>(this Tree<T> tree, int maxDepth)
        {
            return EvaluateHelper(tree, TreeMapInfo.Root(), maxDepth);
        }

        private static Tree<T> EvaluateHelper<T>(this Tree<T> tree, TreeMapInfo info, int maxDepth)
        {
            if (info.Depth > maxDepth)
                return tree;

            if (tree?.Children == null)
                return tree;

            var children = tree.Children.Select((c, i) => EvaluateHelper(c, info.Child(i), maxDepth)).ToList();
            return new Tree<T>(tree.Value, children);
        }
    }
}
