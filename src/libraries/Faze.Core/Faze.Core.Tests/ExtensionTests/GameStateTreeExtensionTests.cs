using Faze.Abstractions.GameMoves;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.ExtensionTests
{
    public class GameStateTreeExtensionTests
    {
        [Fact]
        public void ToStateTreeNull()
        {
            TestGameState<GridMove, int?> state = null;

            var tree = state.ToStateTree();
        }

        [Fact]
        public void ToStateTree()
        {
            var state = new TestGameState<GridMove, int?>
            {
                AvailableMoves = new GridMove[] { 0, 2, 3 }
            };

            var tree = state.ToStateTree();
        }

        [Fact]
        public void ToStateTreeWithAdapter()
        {
            var state = new TestGameState<GridMove, int?>
            {
                AvailableMoves = new GridMove[] { 0, 2, 3 }
            };

            var tree = state.ToStateTree(new SquareTreeAdapter(2));
        }

        [Fact]
        public void ToPathTree()
        {
            var state = new TestGameState<GridMove, int?>
            {
                AvailableMoves = new GridMove[] { 0, 2 }
            };

            var tree = state.ToPathTree();
            var expected = new []
            {
                Array.Empty<GridMove>(),
                new GridMove[]{ 0 },
                new GridMove[]{ 2 },
                new GridMove[]{ 0, 0 },
                new GridMove[]{ 0, 2 },
                new GridMove[]{ 2, 0 },
                new GridMove[]{ 2, 2 },
            };
            tree.SelectBreadthFirst().Take(7).ShouldBe(expected);
        }
    }
}
