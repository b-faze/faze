using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Utilities.Testing;
using Xunit;
using Faze.Core.TreeLinq;
using Shouldly;
using System.Linq;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class LimitDepthTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree1Depth0Id = "tree1_depth0.json";
        private const string Tree1Depth1Id = "tree1_depth1.json";
        private const string Tree1Depth2Id = "tree1_depth2.json";
        private const string Tree1Depth3Id = "tree1_depth3.json";
        private const string Tree1Depth4Id = "tree1_depth4.json";
        private readonly IFileTreeDataProvider<int?> treeDataProvider;

        public LimitDepthTests()
        {
            var basePath = @"../../../Resources/TreeLinq/LimitDepthTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
        }

        [Theory]
        [InlineData(Tree1Id, 0, Tree1Depth0Id)]
        [InlineData(Tree1Id, 1, Tree1Depth1Id)]
        [InlineData(Tree1Id, 2, Tree1Depth2Id)]
        [InlineData(Tree1Id, 3, Tree1Depth3Id)]
        [InlineData(Tree1Id, 4, Tree1Depth4Id)]
        public void LimitDepthTestCases(string baseTreeId, int depth, string expectedTreeId)
        {
            var tree = treeDataProvider.Load(baseTreeId);
            var expectedTree = treeDataProvider.Load(expectedTreeId);

            var actualTree = tree.LimitDepth(depth);

            actualTree.Equals(expectedTree).ShouldBeTrue();
        }
    }
}
