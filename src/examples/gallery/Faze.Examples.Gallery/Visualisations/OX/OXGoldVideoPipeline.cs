using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Examples.Gallery.Visualisations.OX.DataGenerators;
using Faze.Rendering.ColorInterpolators;
using Faze.Rendering.TreeRenderers;
using System;
using Faze.Core.TreeLinq;
using Faze.Rendering.Video.Extensions;
using Faze.Examples.Games.OX;
using Faze.Core.Adapters;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Gallery.Interfaces;
using Faze.Examples.Gallery.Extensions;
using Faze.Examples.Gallery.Services;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideoPipelineConfig : ISquareTreeRendererPipelineConfig
    {
        public int TreeSize { get; set; }
        public int ImageSize { get; set; }
        public float BorderProportion { get; set; }
        public int MaxDepth { get; set; }

        public int LeafSimulations { get; set; }
    }
    public class OXGoldVideoPipeline : BaseVisualisationPipeline<OXGoldVideoPipelineConfig>
    {
        private readonly IGalleryService galleryService;

        public OXGoldVideoPipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public static readonly string Id = "OX Gold Video";
        public override string GetId() => Id;
        public override string GetDataId() => null;

        public override IPipeline Create(GalleryItemMetadata<OXGoldVideoPipelineConfig> galleryMetadata)
        {
            var config = galleryMetadata.Config;

            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetadata)
                .Merge()
                .Map(builder => builder
                    .Render(new SquareTreeRenderer(config.GetRendererOptions()))
                    .Paint(new GoldInterpolator())
                    .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                )
                .Iterate(new WinLoseDrawResultsTreeIterater(new GameSimulator(), config.LeafSimulations))
                .LimitDepth(config.MaxDepth)
                .GameTree(new SquareTreeAdapter(3))
                .Build(() => OXState.Initial);
        }
    }
}
