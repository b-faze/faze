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

namespace Faze.Core.Tests.TreeMapperTests
{
    public class MoveOrderInvariantTreeMapperTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree1ExpectedId = "tree1_expected.json";
        private const string Tree2Id = "tree2.json";
        private const string Tree2ExpectedId = "tree2_expected.json";
        private const string Tree3Id = "tree3.json";
        private const string Tree3ExpectedId = "tree3_expected.json";
        private const string Tree4Id = "tree4.json";

        private readonly IFileTreeDataProvider<int?> treeDataProvider;
        private readonly MoveOrderInvariantTreeMapper treeMapper;

        public MoveOrderInvariantTreeMapperTests()
        {
            var basePath = @"../../../Resources/TreeMapper/MoveOrderInvariantTreeMapperTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
            this.treeMapper = new MoveOrderInvariantTreeMapper();
        }

        [Theory]
        [InlineData(Tree1Id, Tree1ExpectedId)]
        [InlineData(Tree2Id, Tree2ExpectedId)]
        [InlineData(Tree3Id, Tree3ExpectedId)]
        public void TestCases(string treeId, string expectedTreeId)
        {
            var tree = treeDataProvider.Load(treeId);
            var expectedTree = treeDataProvider.Load(expectedTreeId);

            var actualTree = treeMapper.Map(tree, NullProgressTracker.Instance);

            actualTree.SelectDepthFirst().Where(x => x != null).ShouldBe(expectedTree.SelectDepthFirst().Where(x => x != null));
            actualTree.Equals(expectedTree).ShouldBeTrue();
        }

        [Theory]
        [InlineData(Tree1Id, 4)]
        [InlineData(Tree2Id, 8)]
        [InlineData(Tree3Id, 13)]
        [InlineData(Tree4Id, 21)]
        public void OnlyEvaluatesUniquePaths(string treeId, int uniquePathCount)
        {
            var progress = NullProgressTracker.Instance;

            var runMapper1 = new TestRunCountTreeMapper();
            var tree1 = runMapper1.Map(treeDataProvider.Load(treeId), progress);
            var leaves1 = tree1.GetLeaves().Count();

            var runMapper2 = new TestRunCountTreeMapper();
            var tree2 = runMapper2.Map(treeDataProvider.Load(treeId), progress);
            var mappedTree2 = treeMapper.Map(tree2, progress);
            var leaves2 = mappedTree2.GetLeaves().Count();

            runMapper2.NodeRunCount.ShouldBe(uniquePathCount);
        }
    }
}
