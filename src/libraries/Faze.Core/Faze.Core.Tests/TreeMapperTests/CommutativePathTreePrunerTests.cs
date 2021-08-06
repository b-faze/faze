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
using Faze.Core.Extensions;

namespace Faze.Core.Tests.TreeMapperTests
{
    public class CommutativePathTreePrunerTests
    {
        private readonly DynamicTreeDataProvider<string> treeDataProvider;
        private readonly CommutativePathTreePruner treeMapper;

        public CommutativePathTreePrunerTests()
        {
            this.treeDataProvider = new DynamicTreeDataProvider<string>();
            this.treeMapper = new CommutativePathTreePruner();
        }

        [Theory]
        [InlineData(2, 3, 15, 10)]
        [InlineData(2, 5, 63, 21)]
        public void TestCases(int branchingFactor, int depth, int originalCount, int expectedCount)
        {
            var tree = treeDataProvider.Load(new DynamicTreeOptions<string>(branchingFactor, depth, info => $"[{string.Join(",", info.GetPath())}]"));

            tree.SelectDepthFirst().Count().ShouldBe(originalCount);

            var actualTree = treeMapper.Map(tree);

            actualTree.SelectDepthFirst().Count().ShouldBe(expectedCount);
            actualTree.SelectDepthFirst().Count().ShouldBe(expectedCount, "expected same node count on second iteration");
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(2, 5)]
        [InlineData(3, 4)]
        public void CanRestore(int branchingFactor, int depth)
        {
            var merger = new CommutativePathTreeMerger();
            var pruner = new CommutativePathTreePruner();

            var originalTree = treeDataProvider.Load(new DynamicTreeOptions<string>(branchingFactor, depth, info => $"[{info.GetPathHash()}]"));

            var prunedTree = pruner.Map(originalTree);
            var mergedTree = merger.Map(originalTree);

            var restoredTree = merger.Map(prunedTree);

            restoredTree.Equals(mergedTree).ShouldBeTrue();
        }
    }
}
