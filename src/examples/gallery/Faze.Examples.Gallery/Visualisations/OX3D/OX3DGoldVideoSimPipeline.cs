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
using Faze.Examples.Games.GridGames;
using Faze.Examples.Gallery.Visualisations.OX;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldVideoSimPipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }

        public int LeafSimulations { get; set; }
    }
    public class OX3DGoldVideoSimPipeline : BaseVisualisationPipeline<OX3DGoldVideoSimPipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public OX3DGoldVideoSimPipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "OX3D Gold Video";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/OX3D/OX3DGoldVideoPipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OX3DGoldVideoSimPipelineConfig> galleryMetadata)
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
                .Build(() => OX3DState.Initial());
        }
    }
}
