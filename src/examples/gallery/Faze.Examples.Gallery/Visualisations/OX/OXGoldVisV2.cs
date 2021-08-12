using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;
using System.IO;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVisV2 : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly OXGoldImagePipeline pipelineProvider;

        public OXGoldVisV2(IGalleryService galleryService, OXGoldImagePipeline pipelineProvider)
        {
            this.galleryService = galleryService;
            this.pipelineProvider = pipelineProvider;
        }

        public ImageGeneratorMetaData GetMetaData()
        {
            return new ImageGeneratorMetaData(new[] { Albums.OX });
        }

        public Task Generate(IProgressTracker progress)
        {
            var maxDepth = 5;
            progress.SetMaxTicks(maxDepth);

            for (var i = 1; i <= maxDepth; i++)
            {
                Run(progress, i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressTracker progress, int depth)
        {
            var id = $"V2 OX Gold {depth}.png";
            progress.SetMessage(id);

            var metaData = new GalleryItemMetadata
            {
                FileId = id,
                Album = Albums.OX,
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(3, 500)
            {
                MaxDepth = depth,
                BorderProportion = 0.07f
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, x => x.GetWinsOverLoses());
            pipeline.Run(progress);

            return Task.CompletedTask;
        }
    }

}
