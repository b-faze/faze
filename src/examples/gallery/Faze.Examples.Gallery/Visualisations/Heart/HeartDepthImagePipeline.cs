using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;
using Faze.Examples.Games.Heart;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartDepthImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
        public float? RelativeDepthFactor { get; set; }
    }

    public class HeartDepthImagePipeline : BaseVisualisationPipeline<HeartDepthImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public HeartDepthImagePipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "Heart Depth";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            RelativeCodePath = "Visualisations/Heart/HeartDepthImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<HeartDepthImagePipelineConfig> galleryMetadata)
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            var config = galleryMetadata.Config;
            var maxDepth = config.MaxDepth ?? 1;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new TurboInterpolator())
                .Map<double, IGameState<GridMove, WinLoseDrawResult?>>(t => t.MapValue((_, info) => (double)info.Depth / maxDepth))
                .LimitDepth(maxDepth)
                .GameTree(new SquareTreeAdapter(2))
                .Build(() => HeartState.Initial(p1Hand, p2Hand));
        }
    }
}
