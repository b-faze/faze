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

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemImagePipeline : IVisualisationPipeline<EightQueensProblemImagePipelineConfig>
    {
        private static readonly string DataId = EightQueensProblemExhaustiveDataPipeline.Id;
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider;

        public EightQueensProblemImagePipeline(IGalleryService galleryService, IFileTreeDataProvider<EightQueensProblemSolutionAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "Eight Queens Problem";

        string IVisualisationPipeline.Id => Id;
        string IVisualisationPipeline.DataId => DataId;

        public IPipeline Create(GalleryItemMetadata galleryMetaData)
        {
            if (galleryMetaData is GalleryItemMetadata<EightQueensProblemImagePipelineConfig> typedMetaData)
            {
                return Create(typedMetaData);
            }

            throw new NotSupportedException($"'{nameof(galleryMetaData)}' must be of generic type '{typeof(EightQueensProblemImagePipelineConfig)}'");
        }

        public IPipeline Create(GalleryItemMetadata<EightQueensProblemImagePipelineConfig> galleryMetaData)
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
