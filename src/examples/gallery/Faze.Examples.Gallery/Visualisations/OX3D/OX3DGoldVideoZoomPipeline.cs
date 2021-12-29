using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using Faze.Core.TreeLinq;
using Faze.Examples.Games.OX;
using Faze.Core.Adapters;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Abstractions.GameMoves;
using System;
using Faze.Abstractions.Rendering;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.OX3D
{
    public class OX3DGoldVideoZoomPipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }

        public int TotalFrames { get; set; }
        public int[] ZoomPath { get; set; }
    }
    public class OX3DGoldVideoZoomPipeline : BaseVisualisationPipeline<OX3DGoldVideoZoomPipelineConfig>
    {
        private static readonly string DataId = OXDataGeneratorExhaustive.Id;
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OX3DGoldVideoZoomPipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "OX3D Gold Zoom Video";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = DataId,
            RelativeCodePath = "Visualisations/OX3D/OX3DGoldVideoPipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OX3DGoldVideoZoomPipelineConfig> galleryMetadata)
        {
            var dimension = 3;
            var config = galleryMetadata.Config;
            var rendererOptions = config.GetRendererOptions();

            IViewport originalViewport = rendererOptions.Viewport;
            var maxDepth = config.ZoomPath.Length;
            var nextViewport = GetViewportGenerator(config.ZoomPath, dimension, config.BorderProportion);

            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetadata)
                .Merge()
                .Map(builder => builder
                    .Render(new SquareTreeRenderer(rendererOptions))
                )
                .Iterate(config.TotalFrames, progress =>
                {
                    rendererOptions.Viewport = nextViewport(rendererOptions.Viewport, progress);
                })
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                .LoadTree(DataId, treeDataProvider);
        }

        private static Func<IViewport, float, IViewport> GetViewportGenerator(int[] path, int dimension, float borderProportion)
        {
            var maxDepth = path.Length;
            var finalViewport = SquareTreeRendererExtensions.GetFinalViewport(path, dimension, borderProportion);
            var zoomCenterX = finalViewport.Left + finalViewport.Scale / 2;
            var zoomCenterY = finalViewport.Top + finalViewport.Scale / 2;

            return (IViewport viewport, float progress) =>
            {
                var currentSize = (float)Math.Pow(dimension, progress * maxDepth);
                var newScale = 1 / currentSize;
                viewport = viewport.Zoom(zoomCenterX, zoomCenterY, newScale);
                var centerX = viewport.Left + viewport.Scale / 2;
                var centerY = viewport.Top + viewport.Scale / 2;
                var offsetX = zoomCenterX - centerX;
                var offsetY = zoomCenterY - centerY;
                // try to center screen over zoom point
                return viewport.Pan(offsetX, offsetY);
            };

        }


    }
}
