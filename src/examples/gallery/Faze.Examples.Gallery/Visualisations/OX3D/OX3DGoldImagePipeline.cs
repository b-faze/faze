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

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldImagePipeline
    {
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OX3DGoldImagePipeline(IGalleryService galleryService,
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
                .LoadTree(OX3DDataGenerator6.Id, treeDataProvider);
        }
    }
}
