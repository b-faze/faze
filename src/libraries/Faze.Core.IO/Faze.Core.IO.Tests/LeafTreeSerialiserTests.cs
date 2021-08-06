using Faze.Abstractions.Core;
using Faze.Core.Serialisers;
using Faze.Core.TreeLinq;
using Shouldly;
using System;
using System.IO;
using Xunit;

namespace Faze.Core.IO.Tests
{
    public class LeafTreeSerialiserTests
    {
        private readonly ITreeSerialiser<int?> treeSerialiser;

        public LeafTreeSerialiserTests()
        {
            this.treeSerialiser = new LeafTreeSerialiser<int?>(new NullableIntSerialiser());
        }

        [Fact]
        public void CanWriteAndReadTree()
        {
            var child1 = new Tree<int?>(1);
            var child2 = new Tree<int?>(2);
            var child3 = new Tree<int?>(3);

            var tree = new Tree<int?>(null, new[] { child1, child2, child3 });

            byte[] content;

            using (var ms = new MemoryStream())
            using (var ts = new StreamWriter(ms))
            {
                treeSerialiser.Serialize(ts, tree);
                ts.Flush();
                content = ms.ToArray();
            }

            Tree<int?> deserialisedTree;
            using (var ms = new MemoryStream(content))
            using (var tr = new StreamReader(ms))
            {
                deserialisedTree = treeSerialiser.Deserialize(tr);
            }

            deserialisedTree.ShouldBe(tree);

        }
    }
}
