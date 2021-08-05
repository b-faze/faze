using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System;
using System.Linq;

namespace Faze.Core.TreeMappers
{
    public class AggregateTreeMapper<T> : ITreeMapper<T, T> where T : IResultAggregate<T>
    {
        private readonly int depth;

        public AggregateTreeMapper(int depth)
        {
            this.depth = depth;
        }

        public Tree<T> Map(Tree<T> tree, IProgressTracker progress)
        {
            return MapHelper(tree, TreeMapInfo.Root(), progress);
        }

        public Tree<T> MapHelper(Tree<T> tree, TreeMapInfo info, IProgressTracker progress)
        {
            if (info.Depth == 3)
                progress.SetMessage(string.Join(",", info.GetPath()));

            if (tree == null)
                return null;

            if (tree.Children == null)
                return tree;

            var value = tree.Value;
            var children = tree.Children.Select((child, i) => MapHelper(child, info.Child(i), progress));
            foreach (var child in children)
            {
                if (child == null) continue;

                value.Add(child.Value);
            }

            //if (info.Depth <= depth)
            //    return new Tree<T>(value, children.ToList());

            return tree;
            //return new Tree<T>(tree.Value, children); //TODO change to stop modification of the original tree value
        }
    }
}
