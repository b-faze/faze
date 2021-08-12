using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;
using System.IO;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVideoV1: IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly OXGoldVideoPipeline pipelineProvider;

        public OXGoldVideoV1(IGalleryService galleryService, OXGoldVideoPipeline pipelineProvider)
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
            var maxDepth = 3;
            var id = $"OX Gold {maxDepth}.mp4";
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
                BorderProportion = 0.07f
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, maxDepth, 100);
            pipeline.Run(progress);

            return Task.CompletedTask;
        }
    }

}
