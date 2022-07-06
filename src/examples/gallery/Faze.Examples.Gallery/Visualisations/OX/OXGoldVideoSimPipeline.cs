using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;
using Faze.Examples.Games.OX;
using Faze.Core.Adapters;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideoSimPipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }

        public int LeafSimulations { get; set; }
        public float? RelativeDepthFactor { get; set; }
    }
    public class OXGoldVideoSimPipeline : BaseVisualisationPipeline<OXGoldVideoSimPipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public OXGoldVideoSimPipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "OX Gold Video";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/OX/OXGoldVideoPipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OXGoldVideoSimPipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetadata)
                .Merge()
                .Map(builder => builder
                    .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                    .Paint(new GoldInterpolator())
                    .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                )
                .Iterate(new WinLoseDrawResultsTreeIterator(new GameSimulator(), config.LeafSimulations))
                .LimitDepth(config.MaxDepth.Value)
                .GameTree(new SquareTreeAdapter(3))
                .Build(() => OXState.Initial);
        }
    }
}
