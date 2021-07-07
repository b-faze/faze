using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Engine.Players;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeLinq;
using Faze.Rendering.TreeRenderers;
using System.Linq;
using Xunit;
using Faze.Rendering.Extensions;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;

namespace Faze.Examples.OX.Tests
{
    public class OXStateGalleryTests
    {
        private readonly IGalleryService galleryService;

        public OXStateGalleryTests()
        {
            this.galleryService = new GalleryService(new GalleryServiceConfig
            {
                BasePath = @"../../../gallery"
            });
        }

        [Fact]
        public void OXGold1()
        {
            var p1 = new MonkeyAgent();
            var p2 = new MonkeyAgent();
            var players = new[] { p1, p2 };
            IGameState<GridMove, WinLoseDrawResult?> state = OXState.Initial;

            var maxDepth = 3;

            var rendererOptions = new SquareTreeRendererOptions(3)
            {
                BorderProportions = 0,
                MaxDepth = maxDepth
            };

            IPaintedTreeRenderer renderer = new SquareTreeRenderer(rendererOptions, 500);

            var engine = new GameSimulator();

            WinLoseDrawResultAggregate MapTree(Tree<IGameState<GridMove, WinLoseDrawResult?>> node) 
            {
                if (node.IsLeaf())
                {
                    var simulations = 100;
                    var resultAggregate = new WinLoseDrawResultAggregate();
                    var results = engine
                        .SampleResults(node.Value, players, simulations)
                        .Where(x => x != null)
                        .Select(x => (WinLoseDrawResult)x);

                    resultAggregate.AddRange(results);

                    return resultAggregate;
                }
                else
                {
                    var results = new WinLoseDrawResultAggregate();

                    foreach (var child in node.Children.Where(x => x != null))
                    {
                        var childResults = MapTree(child);

                        results.Add(childResults);
                    }

                    return results;
                }
            }

            var visibleTree = renderer.GetVisible(state.ToStateTree(move => move, 9));
            var resultsTree = visibleTree
                            .MapTree(MapTree)
                            .MapValue(x => (double)x.Wins / (x.Wins + x.Loses));

            var renderTree = resultsTree.MapValue(new GoldInterpolator());

            renderer.Draw(renderTree);
            galleryService.Save(renderer, new GalleryItemMetadata
            {
                Id = "OXGold1",
                FileName = "OX Gold 1.png",
                Albums = new[] { "OX" },
                Description = "Desc",
            });
        }

    }
}
