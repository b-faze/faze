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
    public class OXGoldPipeline
    {
        private readonly IGalleryService galleryService;
        private readonly ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXGoldPipeline(IGalleryService galleryService,
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
    }
}
