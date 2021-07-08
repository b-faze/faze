using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Engine.Benchmarks.TreeLinqBenchmarks
{
    public static class TreeUtilities
    {
        public static Tree<T> CreateTree<T>(int branchingFactor, int depth)
        {
            return CreateTree<T>(branchingFactor, depth, 0);
        }

        private static Tree<T> CreateTree<T>(int branchingFactor, int maxDepth, int currentDepth)
        {
            if (currentDepth == maxDepth)
                return new Tree<T>(default(T));

            var children = Enumerable.Range(0, branchingFactor).Select(i => CreateTree<T>(branchingFactor, maxDepth, currentDepth + 1));

            return new Tree<T>(default(T), children);
        }
    }
}
