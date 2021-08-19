using Faze.Core.Data;
using Faze.Core.TreeLinq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.TreeTests
{
    public class DynamicTreeTests
    {
        [Fact]
        public void CanCreateBinaryTree()
        {
            var provider = new DynamicTreeDataProvider<int>();
            var binaryTreeOptions = new DynamicTreeOptions<int>(2, 3, info => info.Depth);

            var binaryTree = provider.Load(binaryTreeOptions);

            var expectedValues = new[] { 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3 };
            binaryTree.SelectBreadthFirst().ShouldBe(expectedValues);
        }

        [Fact]
        public void CanCreateSquareTree()
        {
            var size = 2;
            var depth = 2;
            var provider = new DynamicTreeDataProvider<int>();
            var binaryTreeOptions = new DynamicSquareTreeOptions<int>(size, depth, info => info.Depth);

            var binaryTree = provider.Load(binaryTreeOptions);

            var values = binaryTree.SelectBreadthFirst().ToArray();

            for (var d = 0; d < depth; d++)
            {
                values.Count(c => c == d).ShouldBe((int)Math.Pow(size * size, d));
            }

        }
    }
}
