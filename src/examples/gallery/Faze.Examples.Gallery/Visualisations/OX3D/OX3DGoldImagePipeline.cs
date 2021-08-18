using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Core.TreeLinq;
using Faze.Examples.Gallery.Visualisations.OX3D.DataGenerators;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldImagePipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }
    }

    public class OX3DGoldImagePipeline : BaseVisualisationPipeline<OX3DGoldImagePipelineConfig>
    {
        private static readonly string DataId = OX3DDataGenerator6.Id;

        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OX3DGoldImagePipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "OX3D Gold";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = DataId,
            RelativeCodePath = "Visualisations/OX3D/OX3DGoldImagePipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OX3DGoldImagePipelineConfig> galleryMetaData)
        {
            var config = galleryMetaData.Config;

            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                .LoadTree(OX3DDataGenerator6.Id, treeDataProvider);
        }
    }
}
