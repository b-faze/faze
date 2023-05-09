using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartGoldImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
        public float? RelativeDepthFactor { get; set; }
    }

    public class HeartGoldImagePipeline : BaseVisualisationPipeline<HeartGoldImagePipelineConfig>
    {

        public static readonly string Id = "Heart Gold";

        private static readonly string DataId = HeartDataGeneratorExhaustive.Id;
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public HeartGoldImagePipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = DataId,
            RelativeCodePath = "Visualisations/Heart/HeartGoldImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<HeartGoldImagePipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                .LoadTree(DataId, treeDataProvider);
        }
    }
}
