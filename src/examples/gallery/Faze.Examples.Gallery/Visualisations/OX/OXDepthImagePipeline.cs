using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;
using Faze.Core.Adapters;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameStates;
using Faze.Examples.Games.OX;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXDepthImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
        public float? RelativeDepthFactor { get; set; }
    }

    public class OXDepthImagePipeline : BaseVisualisationPipeline<OXDepthImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public OXDepthImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "OX Depth";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            RelativeCodePath = "Visualisations/OX/OXDepthImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OXDepthImagePipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;
            var maxDepth = config.MaxDepth ?? 1;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new TurboInterpolator())
                .Map<double, IGameState<GridMove, WinLoseDrawResult?>>(t => t.MapValue((_, info) => (double)info.Depth / maxDepth))
                .LimitDepth(maxDepth)
                .GameTree(new SquareTreeAdapter(3))
                .Build(() => OXState.Initial);
        }
    }

}
