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

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideoPipeline
    {
        private readonly IGalleryService galleryService;

        public OXGoldVideoPipeline(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IPipeline Create(GalleryItemMetadata galleryMetaData, SquareTreeRendererOptions rendererConfig, int maxDepth, int leafSimulations)
        {
            return ReversePipelineBuilder.Create()
                .GalleryVideo(galleryService, galleryMetaData)
                .MergeStreamers()
                .Map(builder => builder
                    .StreamRender()
                    .Render(new SquareTreeRenderer(rendererConfig))
                    .Paint(new GoldInterpolator())
                    .Map<double, WinLoseDrawResultAggregate>(t => t.MapValue(v => v.GetWinsOverLoses()))
                )
                .Iterate(new WinLoseDrawResultsTreeIterater(new GameSimulator(), leafSimulations))
                .LimitDepth(maxDepth)
                .GameTree(new SquareTreeAdapter(3))
                .Build(() => OXState.Initial);
        }
    }
}
