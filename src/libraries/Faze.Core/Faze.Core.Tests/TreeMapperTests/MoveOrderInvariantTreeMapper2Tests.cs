using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Core.TreeMappers;
using Faze.Core.TreeLinq;
using Faze.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Faze.Core.Data;

namespace Faze.Core.Tests.TreeMapperTests
{
    public class MoveOrderInvariantTreeMapper2Tests
    {
        private readonly DynamicTreeDataProvider<string> treeDataProvider;
        private readonly MoveOrderInvariantTreeMapper2 treeMapper;

        public MoveOrderInvariantTreeMapper2Tests()
        {
            var basePath = @"../../../Resources/TreeMapper/MoveOrderInvariantTreeMapper2Tests";

            this.treeDataProvider = new DynamicTreeDataProvider<string>();
            this.treeMapper = new MoveOrderInvariantTreeMapper2();
        }

        [Theory]
        [InlineData(2, 3, 10)]
        public void TestCases(int branchingFactor, int depth, int expectedCount)
        {
            var tree = treeDataProvider.Load(new DynamicTreeOptions<string>(branchingFactor, depth, info => $"[{string.Join(",", info.GetPath())}]"));

            var actualTree = treeMapper.Map(tree, NullProgressTracker.Instance);

            actualTree.SelectDepthFirst().Count().ShouldBe(expectedCount);
            actualTree.SelectDepthFirst().Count().ShouldBe(expectedCount, "expected same node count on second iteration");
        }

        [Theory]
        [InlineData(2, 3)]
        public void CanReverse(int branchingFactor, int depth)
        {
            var mapper1 = new MoveOrderInvariantTreeMapper();
            var mapper2 = new MoveOrderInvariantTreeMapper2();

            var tree = treeDataProvider.Load(new DynamicTreeOptions<string>(branchingFactor, depth, info => $"[{string.Join(",", info.GetPath())}]"));

            var reducedTree = mapper2.Map(tree, NullProgressTracker.Instance);
            var shortcutTree = mapper1.Map(tree, NullProgressTracker.Instance);

            var restoredTree = mapper1.Map(reducedTree, NullProgressTracker.Instance);

            restoredTree.Equals(shortcutTree).ShouldBeTrue();
        }
    }
}
