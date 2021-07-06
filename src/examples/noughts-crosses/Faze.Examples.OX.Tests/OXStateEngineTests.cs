using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Players;
using Faze.Engine.Simulators;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.OX.Tests
{
    public class OXStateEngineTests
    {
        private readonly Random rnd;

        public OXStateEngineTests()
        {
            this.rnd = new Random(Seed: 25567);
        }

        [Fact]
        public void Game1()
        {
            var engine = new GameSimulator();
            var p1 = new MonkeyAgent(rnd);
            var p2 = new MonkeyAgent(rnd);
            IGameState<int, WinLoseDrawResult?, IPlayer> state = OXState<IPlayer>.Initial(p1, p2);

            state = state.Move(0);
            state.GetResult().ShouldBeNull();

            state = state.Move(3);
            state.GetResult().ShouldBeNull();

            state = state.Move(1);
            state.GetResult().ShouldBeNull();

            state = state.Move(8);
            state.GetResult().ShouldBeNull();

            var results = engine.SampleResults(state, 1000).ToArray();

            results.Count(x => x == WinLoseDrawResult.Win).ShouldBe(673);
            results.Count(x => x == WinLoseDrawResult.Lose).ShouldBe(129);
            results.Count(x => x == WinLoseDrawResult.Draw).ShouldBe(198);
        }

    }
}
