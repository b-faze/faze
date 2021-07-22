using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Utilities.Testing;
using Xunit;
using Faze.Core.TreeLinq;
using Shouldly;
using Faze.Abstractions.GameResults;
using System.Linq;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class MapValueAggTests
    {
        private const string Tree1Id = "tree1.json";
        private const string Tree1AggId = "tree1_agg.json";
        private readonly IFileTreeDataProvider<int?> treeDataProvider;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> outputTreeDataProvider;

        public MapValueAggTests()
        {
            var basePath = @"../../../Resources/TreeLinq/MapValueAggTests";
            this.treeDataProvider = new TestFileTreeDataProvider<int?>(basePath, new NullableIntSerialiser());
            this.outputTreeDataProvider = new TestFileTreeDataProvider<WinLoseDrawResultAggregate>(basePath, new WinLoseDrawResultAggregateSerialiser());
        }

        [Fact]
        public void CanMapValueAgg()
        {
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = outputTreeDataProvider.Load(Tree1AggId);

            var mappedTree = tree.MapValueAgg(MapValue, () => new WinLoseDrawResultAggregate());

            mappedTree.Equals(expectedTree).ShouldBeTrue();
        }

        [Fact]
        public void OnlyEnumerateTreeOnce()
        {
            int actualCount = 0;
            var tree = treeDataProvider.Load(Tree1Id);
            var expectedTree = outputTreeDataProvider.Load(Tree1AggId);

            var expectedCount = tree.GetLeaves().Count();
            var mappedTree = tree.MapValueAgg(x => MapValueWithCount(x, ref actualCount), () => new WinLoseDrawResultAggregate());

            actualCount.ShouldBe(expectedCount);
        }

        private WinLoseDrawResultAggregate MapValue(int? value)
        {
            return new WinLoseDrawResultAggregate((uint)(value ?? 0), 0, 0);
        }

        private WinLoseDrawResultAggregate MapValueWithCount(int? value, ref int count)
        {
            count++;
            return new WinLoseDrawResultAggregate((uint)(value ?? 0), 0, 0);
        }
    }
}
