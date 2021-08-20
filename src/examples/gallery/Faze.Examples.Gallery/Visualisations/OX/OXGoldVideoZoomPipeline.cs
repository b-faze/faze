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

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideoZoomPipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int? MaxDepth { get; set; }
        public int[] ZoomPath { get; set; }
    }
    public class OXGoldVideoZoomPipeline : BaseVisualisationPipeline<OXGoldVideoZoomPipelineConfig>
    {
        private static readonly string DataId = OXDataGeneratorExhaustive.Id;
        private readonly IGalleryService galleryService;
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXGoldVideoZoomPipeline(IGalleryService galleryService,
            IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.galleryService = galleryService;
            this.treeDataProvider = treeDataProvider;
        }

        public static readonly string Id = "OX Gold Zoom Video";
        public override GalleryPipelineMetadata GetMetadata() => new GalleryPipelineMetadata
        {
            Id = Id,
            DataId = DataId,
            RelativeCodePath = "Visualisations/OX/OXGoldVideoPipeline.cs"
        };

        public override IPipeline Create(GalleryItemMetadata<OXGoldVideoZoomPipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;
            var rendererOptions = config.GetRendererOptions();
            var zoomScale = 1f;
            var (zoomX, zoomY, targetZoomScale) = GetFinalViewport(config.ZoomPath, 3, rendererOptions.BorderProportion);
            var totalFrames = 400;

            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetadata)
                .Merge()
                .Map(builder => builder
                    .Render(new SquareTreeRenderer(rendererOptions))
                )
                .Iterate(totalFrames, () =>
                {
                    zoomScale *= 0.99f;
                    rendererOptions.Viewport = rendererOptions.Viewport.Zoom(zoomX, zoomY, zoomScale);
                })
                .Paint(new GoldInterpolator())
                .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                .LoadTree(DataId, treeDataProvider);
        }

        private static (float x, float y, float zoom) GetFinalViewport(int[] path, int dimension, float borderProportion)
        {
            float x = 0;
            float y = 0;

            int depth = 0;
            float scale = 1;
            foreach (GridMove moveIndex in path)
            {
                // border compensation
                x += 1 / (float)Math.Pow(dimension, depth) * borderProportion;
                y += 1 / (float)Math.Pow(dimension, depth) * borderProportion;

                depth++;

                float dx = moveIndex.GetX(dimension);
                float dy = moveIndex.GetY(dimension);

                scale = (float)Math.Pow(dimension, depth);
                x += dx / scale;
                y += dy / scale;
            }

            //center zoom
            x += 1 / (scale * 2);
            y += 1 / (scale * 2);

            return (x, y, 1/scale);
        }
    }
}
