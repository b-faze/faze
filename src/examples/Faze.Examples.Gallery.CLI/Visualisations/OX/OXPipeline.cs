using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.CLI.Interfaces;
using Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX
{
    public class OXPipeline : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly IPaintedTreeRenderer renderer;
        private readonly IColorInterpolator colorInterpolator;
        private readonly ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser;

        public OXPipeline(IGalleryService galleryService,
            IPaintedTreeRenderer renderer,
            IColorInterpolator colorInterpolator,
            ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser)
        {
            this.galleryService = galleryService;
            this.renderer = renderer;
            this.colorInterpolator = colorInterpolator;
            this.treeSerialiser = treeSerialiser;
        }

        public Task Generate()
        {
            var galleryMetaData = new GalleryItemMetadata
            {
                Id = "OXGold1",
                FileName = "OX Gold 5.png",
                Albums = new[] { "OX" },
                Description = "Desc...",
            };

            ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(renderer)
                .Paint(colorInterpolator)
                .MapValue<double, WinLoseDrawResultAggregate>(x => (double)x.Wins / (x.Wins + x.Loses))
                .LoadTree(OXDataGenerator5.Id, treeSerialiser)
                .Run();

            return Task.CompletedTask;
        }

        public IPipeline GetPipeline(IGalleryService galleryService, GalleryItemMetadata metaData)
        {
            var pipeline = ReversePipelineBuilder.Create()
                .GallerySave(galleryService, metaData)
                .Render(renderer)
                .Paint(colorInterpolator)
                .MapValue<double, WinLoseDrawResultAggregate>(x => (double)x.Wins / (x.Wins + x.Loses))
                .LoadTree(filename, treeSerialiser);

            return pipeline;
        }
    }
}
