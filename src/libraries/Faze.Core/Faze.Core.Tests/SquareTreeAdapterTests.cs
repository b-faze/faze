using Faze.Abstractions.GameMoves;
using Faze.Core.Adapters;
using Faze.Core.Tests.Utilities;
using System;
using Xunit;

namespace Faze.Core.Tests
{
    public class SquareTreeAdapterTests
    {
        [Fact]
        public void NullWhenNoAvailableMoves()
        {
            var adapter = new SquareTreeAdapter(3);
            var state = new TestGameState()
            {
                AvailableMoves = new GridMove[0]
            };

            adapter.GetChildren(state);
        }
    }
}
