using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Core.TreeLinq;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldImagePipeline
    {
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXGoldImagePipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public IPipeline Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, Func<WinLoseDrawResultAggregate, double> fn)
        {
            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(fn))
                .LoadTree(OXDataGenerator5.Id, treeDataProvider);
        }

        public IPipeline CreateExhausive(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, Func<WinLoseDrawResultAggregate, double> fn)
        {
            return ReversePipelineBuilder.Create()
                .GalleryImage(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(fn))
                .LoadTree(OXDataGeneratorExhaustive.Id, treeDataProvider);
        }
    }
}
