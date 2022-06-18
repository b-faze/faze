using Faze.Abstractions.Core;
using Faze.Core.Pipelines;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Examples.Gallery.Services.Aggregates;
using Faze.Examples.Gallery.Visualisations.EightQueensProblem.DataGenerators;
using Faze.Core.Extensions;
using Faze.Core.TreeMappers;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
        public bool BlackParentMoves { get; set; }
        public bool BlackUnavailableMoves { get; set; }
        public float? RelativeDepthFactor { get; set; }

        public EightQueensProblemPainterConfig GetPainterConfig()
        {
            return new EightQueensProblemPainterConfig
            {
                BlackParentMoves = BlackParentMoves,
                BlackUnavailableMoves = BlackUnavailableMoves
            };
        }
    }

    public class EightQueensProblemImagePipeline : BaseVisualisationPipeline<EightQueensProblemImagePipelineConfig>
    {
        private static readonly string DataId = EightQueensProblemExhaustiveDataPipeline.Id;
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemImagePipeline(IGalleryService galleryService, IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "8QP 1";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = DataId,
            RelativeCodePath = "Visualisations/EightQueensProblem/EightQueensProblemImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<EightQueensProblemImagePipelineConfig> galleryMetaData)
        {
            var config = galleryMetaData.Config;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new EightQueensProblemPainter(config.GetPainterConfig()))
                .Map(new CommutativePathTreeMerger())
                .LoadTree(DataId, treeDataProvider);
        }


    }
}
