using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;

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
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new GoldInterpolator())
                .MapValue(fn)
                .LoadTree(OXDataGenerator5.Id, treeDataProvider);
        }

        public IPipeline CreateExhausive(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, Func<WinLoseDrawResultAggregate, double> fn)
        {
            return ReversePipelineBuilder.Create()
                .GallerySave(galleryService, galleryMetaData)
                .Render(new SquareTreeRenderer(rendererConfig))
                .Paint(new GoldInterpolator())
                .MapValue(fn)
                .LoadTree(OXDataGeneratorExhaustive.Id, treeDataProvider);
        }
    }
}
