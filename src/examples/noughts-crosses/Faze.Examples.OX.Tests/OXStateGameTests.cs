using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Players;
using Shouldly;
using System;
using Xunit;

namespace Faze.Examples.OX.Tests
{
    public class OXStateGameTests
    {
        [Fact]
        public void Game1()
        {
            IGameState<GridMove, WinLoseDrawResult?> state = OXState.Initial;

            state = state.Move(0);
            state.GetResult().ShouldBeNull();

            state = state.Move(3);
            state.GetResult().ShouldBeNull();

            state = state.Move(1);
            state.GetResult().ShouldBeNull();

            state = state.Move(4);
            state.GetResult().ShouldBeNull();

            state = state.Move(2);
            state.GetResult().ShouldBe(WinLoseDrawResult.Win);
            state.GetAvailableMoves().ShouldBeEmpty();
        }
    }
}
