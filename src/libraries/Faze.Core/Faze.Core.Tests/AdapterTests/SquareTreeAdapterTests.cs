using Faze.Abstractions.GameMoves;
using Faze.Core.Adapters;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.AdapterTests
{
    public class SquareTreeAdapterTests
    {
        [Fact]
        public void CanArrangeChildrenForSize2()
        {
            var state = new TestGameState<GridMove, int?>
            {
                AvailableMoves = new GridMove[] { 0, 2, 3 }
            };

            var size = 2;
            var adapter = new SquareTreeAdapter(size);
            var children = adapter.GetChildren(state).ToArray();

            children.Length.ShouldBe(size * size);
            children[0].ShouldNotBeNull();
            children[1].ShouldBeNull();
            children[2].ShouldNotBeNull();
            children[3].ShouldNotBeNull();
        }
    }
}
