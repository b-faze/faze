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
                BorderProportions = 0
            };

            var renderer = new SquareTreeRenderer(rendererOptions, 500);

            var engine = new GameSimulator();

            (int wins, int total) MapTree(Tree<IGameState<GridMove, WinLoseDrawResult?>> node) 
            {
                if (node == null)
                    return (0, 0);

                if (node.Children == null || node.Children.All(x => x == null))
                {
                    var simulations = 100;
                    var results = engine
                        .SampleResults(node.Value, players, simulations).ToArray();

                    var wins = results.Count(x => x == WinLoseDrawResult.Win);
                    var loses = results.Count(x => x == WinLoseDrawResult.Lose);

                    return (wins, wins + loses);
                }
                else
                {
                    var wins = 0;
                    var simulations = 0;

                    foreach (var child in node.Children)
                    {
                        var (childWins, childSimulations) = MapTree(child);
                        wins += childWins;
                        simulations += childSimulations;
                    }

                    return (wins, simulations);
                }
            }

            var resultsTree = state.ToStateTree(move => move, 9)
                            .LimitDepth(maxDepth)
                            .MapTree(MapTree)
                            .Map(x => (double)x.wins / x.total);

            var renderTree = resultsTree.Map(new GoldInterpolator());

            renderer.Draw(renderTree, Viewport.Default(), maxDepth);
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
