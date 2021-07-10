using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX
{
    public class OXGoldImagePipeline
    {
        private readonly IGalleryService galleryService;
        private readonly ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXGoldImagePipeline(IGalleryService galleryService,
            ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
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
