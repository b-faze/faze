using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Utilities.Testing;
using Xunit;
using Faze.Core.TreeLinq;
using Shouldly;
using System.Linq;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class MapTreeTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree1IncrementedId = "tree1_incremented.json";
        private const string Tree1DepthId = "tree1_depth.json";
        private readonly IFileTreeDataProvider<int?> treeDataProvider;

        public MapTreeTests()
        {
            var basePath = @"../../../Resources/TreeLinq/MapTreeTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
        }

        [Fact]
        public void IsImmutable()
        {
            var tree = treeDataProvider.Load(Tree1Id);

            var mappedTree = tree.MapTree(t => t.Value);

            (tree == mappedTree).ShouldBeFalse();
        }

        [Fact]
        public void CanMapToItself()
        {
            var tree = treeDataProvider.Load(Tree1Id);

            var mappedTree = tree.MapTree(t => t.Value);

            tree.Equals(mappedTree).ShouldBeTrue();
        }

        [Fact]
        public void CanMapTree()
        {
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = treeDataProvider.Load(Tree1IncrementedId);

            var mappedTree = tree.MapTree(t => t.Value + 1);

            mappedTree.Equals(expectedTree).ShouldBeTrue();
        }

        [Fact]
        public void CanMapInfo()
        {
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = treeDataProvider.Load(Tree1DepthId);

            var mappedTree = tree.MapTree((t, info) => (int?)info.Depth);

            mappedTree.Equals(expectedTree).ShouldBeTrue();
        }
    }
}
