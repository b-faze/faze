using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Engine.Players;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery;
using Faze.Rendering.ColorInterpolators;
using Faze.Core.TreeLinq;
using Faze.Rendering.TreeRenderers;
using System.Linq;
using Xunit;
using Faze.Rendering.Extensions;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Core.Pipelines;
using Faze.Rendering.TreePainters;
using Faze.Engine.ResultTrees;

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
            IGameState<GridMove, WinLoseDrawResult?> state = OXState.Initial;

            var maxDepth = 3;

            var rendererOptions = new SquareTreeRendererOptions(3, 500)
            {
                BorderProportions = 0,
                MaxDepth = maxDepth
            };

            var engine = new GameSimulator();


            var galleryMetaData = new GalleryItemMetadata
            {
                Id = "OXGold1",
                FileName = "OX Gold 1.png",
                Albums = new[] { "OX" },
                Description = "Desc",
            };

            ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate> resultsMapper = new WinLoseDrawResultsTreeMapper(engine, 100);

            var pipeline = ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererOptions))
                .Paint(new GoldInterpolator())
                .MapValue<double, WinLoseDrawResultAggregate>(x => (double)x.Wins / (x.Wins + x.Loses))
                .Map(resultsMapper)
                .GameTree(new OXStateTreeAdapter())
                .Build();

            pipeline.Run(state);
        }

        [Fact]
        public void OXDepth1()
        {
            IGameState<GridMove, WinLoseDrawResult?> state = OXState.Initial;

            var rendererOptions = new SquareTreeRendererOptions(3, 500)
            {
                BorderProportions = 0,
                MaxDepth = 3
            };

             var galleryMetaData = new GalleryItemMetadata
            {
                Id = "OXDepth1",
                FileName = "OX Depth 1.png",
                Albums = new[] { "OX" },
                Description = "Desc",
            };

            var pipeline = ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererOptions))
                .Paint<IGameState<GridMove, WinLoseDrawResult?>>(new DepthPainter())
                .GameTree(new OXStateTreeAdapter())
                .Build();

            pipeline.Run(state);
        }

    }
}
