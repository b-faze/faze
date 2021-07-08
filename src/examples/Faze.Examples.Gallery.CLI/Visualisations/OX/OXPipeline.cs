using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX
{
    public class OXPipeline
    {
        private readonly IGalleryService galleryService;
        private readonly IPaintedTreeRenderer renderer;
        private readonly IColorInterpolator colorInterpolator;
        private readonly ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser;
        private readonly string filename;
        private readonly GalleryItemMetadata galleryMetaData;

        public OXPipeline(IGalleryService galleryService,
            IPaintedTreeRenderer renderer,
            IColorInterpolator colorInterpolator,
            ITreeSerialiser<WinLoseDrawResultAggregate> treeSerialiser,
            string filename,
            GalleryItemMetadata galleryMetaData)
        {
            this.galleryService = galleryService;
            this.renderer = renderer;
            this.colorInterpolator = colorInterpolator;
            this.treeSerialiser = treeSerialiser;
            this.filename = filename;
            this.galleryMetaData = galleryMetaData;
        }

        public IPipeline GetPipeline()
        {
            var pipeline = ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(renderer)
                .Paint(colorInterpolator)
                .MapValue<double, WinLoseDrawResultAggregate>(x => (double)x.Wins / (x.Wins + x.Loses))
                .LoadTree(filename, treeSerialiser);

            return pipeline;
        }
    }
}
