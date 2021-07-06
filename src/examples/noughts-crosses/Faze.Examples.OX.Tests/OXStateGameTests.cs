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
            var p1 = new MonkeyAgent();
            var p2 = new MonkeyAgent();
            IGameState<int, WinLoseDrawResult?, IPlayer> state = OXState<IPlayer>.Initial(p1, p2);

            state = state.Move(0);
            state.Result.ShouldBeNull();

            state = state.Move(3);
            state.Result.ShouldBeNull();

            state = state.Move(1);
            state.Result.ShouldBeNull();

            state = state.Move(4);
            state.Result.ShouldBeNull();

            state = state.Move(2);
            state.Result.ShouldBe(WinLoseDrawResult.Win);
        }
    }
}
