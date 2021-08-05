using Faze.Abstractions.Core;
using Faze.Core.TreeLinq;
using System;
using System.Linq;

namespace Faze.Core.Data
{
    public class DynamicTreeOptions<T>
    {
        private readonly int branchingFactor;
        private readonly int depth;
        private readonly Func<TreeMapInfo, T> valueFn;

        public DynamicTreeOptions(int branchingFactor, int depth, Func<TreeMapInfo, T> valueFn)
        {
            this.branchingFactor = branchingFactor;
            this.depth = depth;
            this.valueFn = valueFn;
        }

        public Tree<T> CreateTree()
        {
            return CreateTree(TreeMapInfo.Root());
        }

        private Tree<T> CreateTree(TreeMapInfo info)
        {
            var value = valueFn(info);
            if (info.Depth >= depth)
            {
                return new Tree<T>(value);
            }

            var children = Enumerable.Range(0, branchingFactor).Select(i => CreateTree(info.Child(i)));

            return new Tree<T>(value, children);
        }
    }
}
