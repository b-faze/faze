using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Utilities.Testing;
using Xunit;
using Faze.Core.TreeLinq;
using Shouldly;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class MapValueTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree1IncrementedId = "tree1_incremented.json";
        private const string Tree1DepthId = "tree1_depth.json";
        private readonly IFileTreeDataProvider<int?> treeDataProvider;

        public MapValueTests()
        {
            var basePath = @"../../../Resources/TreeLinq/MapValueTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
        }

        [Fact]
        public void IsImmutable()
        {
            var tree = treeDataProvider.Load(Tree1Id);

            var mappedTree = tree.MapValue(v => v);

            (tree == mappedTree).ShouldBeFalse();
        }

        [Fact]
        public void CanMapToItself()
        {
            var tree = treeDataProvider.Load(Tree1Id);

            var mappedTree = tree.MapValue(v => v);

            tree.Equals(mappedTree).ShouldBeTrue();
        }

        [Fact]
        public void CanMapValue()
        {
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = treeDataProvider.Load(Tree1IncrementedId);

            var mappedTree = tree.MapValue(v => v + 1);

            mappedTree.Equals(expectedTree).ShouldBeTrue();
        }

        [Fact]
        public void CanMapInfo()
        {
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = treeDataProvider.Load(Tree1DepthId);

            var mappedTree = tree.MapValue((v, info) => (int?)info.Depth);

            mappedTree.Equals(expectedTree).ShouldBeTrue();
        }
    }
}
