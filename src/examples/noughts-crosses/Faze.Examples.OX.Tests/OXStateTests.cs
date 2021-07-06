using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Abstractions.Rendering;
using Faze.Engine.Players;
using Faze.Engine.Simulators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using Faze.Rendering.TreeRenderers;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.OX.Tests
{
    public class OXStateTests
    {
        [Fact]
        public void Game1()
        {
            var p1 = new MonkeyAgent();
            var p2 = new MonkeyAgent();
            IGameState<int, WinLoseDrawResult?, IAgent> state = OXState<IAgent>.Initial(p1, p2);

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

        [Fact]
        public void RenderTests()
        {
            var p1 = new MonkeyAgent();
            var p2 = new MonkeyAgent();
            IGameState<int, WinLoseDrawResult?, IAgent> state = OXState<IAgent>.Initial(p1, p2);

            var renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(500), 500);

            var engine = new GameSimulator();

            var resultsTree = state.ToStateTree()
                            .Map(x => engine.SampleResults(x, 100))
                            .Map(xs => (double)xs.Count(x => x == WinLoseDrawResult.Win) / 100);

            var renderTree = resultsTree
                            .Map(new GoldInterpolator());

            renderer.Draw(renderTree, Viewport.Default(), 4);
            renderer.GetBitmap().Save("result.png");
        }
    }
}
