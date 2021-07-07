using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Abstractions.Rendering;
using Faze.Engine.Players;
using Faze.Engine.Simulators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using Faze.Rendering.TreeRenderers;
using System.Linq;
using Xunit;
using Faze.Rendering.Extensions;
using Faze.Abstractions.GameMoves;

namespace Faze.Examples.OX.Tests
{
    public class OXStateRenderTests
    {
        [Fact]
        public void RenderTests()
        {
            var p1 = new MonkeyAgent();
            var p2 = new MonkeyAgent();
            var players = new[] { p1, p2 };
            IGameState<GridMove, WinLoseDrawResult?> state = OXState.Initial;

            var rendererOptions = new SquareTreeRendererOptions(3)
            {
                BorderProportions = 0.1f,
                MaxDepth = 4
            };

            var renderer = new SquareTreeRenderer(rendererOptions, 500);

            var engine = new GameSimulator();

            var resultsTree = state.ToStateTree(move => move, 9)
                            .MapValue(x => engine.SampleResults(x, players, 100))
                            .MapValue(xs => (double)xs.Count(x => x == WinLoseDrawResult.Win) / 100);

            var renderTree = resultsTree
                            .MapValue(new GoldInterpolator());

            renderer.Draw(renderTree);
            renderer.Save("result.png");
        }

    }
}
