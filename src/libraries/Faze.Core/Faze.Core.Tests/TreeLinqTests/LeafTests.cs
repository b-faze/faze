using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Utilities.Testing;
using Xunit;
using Faze.Core.TreeLinq;
using Shouldly;
using System.Linq;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class LeafTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree2Id = "tree2.json";
        private readonly IFileTreeDataProvider<int?> treeDataProvider;

        public LeafTests()
        {
            var basePath = @"../../../Resources/TreeLinq/LeafTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
        }

        [Fact]
        public void CountLeavesOfRootOnly()
        {
            var tree = new Tree<int>(0);

            tree.GetLeaves().Count().ShouldBe(1);
        }

        [Fact]
        public void CountLeavesTree1()
        {
            var tree = treeDataProvider.Load(Tree1Id);

            tree.GetLeaves().Count().ShouldBe(3);
        }

        [Fact]
        public void CountLeavesTree2()
        {
            var tree = treeDataProvider.Load(Tree2Id);

            tree.GetLeaves().Count().ShouldBe(5);
        }
    }
}
