using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Core.TreeLinq;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Gallery.Visualisations.PieceBoards;
using Faze.Examples.Gallery.Visualisations.Rubik.DataGenerators;
using Faze.Examples.Games.Rubik;
using Faze.Rendering.TreeRenderers;

namespace Faze.Examples.Gallery.Visualisations.Rubik
{
    public class RubikImagePipeline : BaseVisualisationPipeline<RubikImagePipelineConfig>
    {
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public RubikImagePipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "Rubik Gold";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = null,
            RelativeCodePath = "Visualisations/Rubik/RubikImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<RubikImagePipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetadata)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new RubikPainter(config.GetPainterConfig()))
                .LimitDepth(config.MaxDepth ?? 0)
                .LoadTree(RubikDataGenerator5.Id, treeDataProvider);
        }
    }
}
