using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using Faze.Rendering.TreeRenderers;
using System.IO;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX
{
    public class OXGoldVis : IImageGenerator
    {
        private readonly IGalleryService galleryService;
        private readonly OXGoldImagePipeline pipelineProvider;

        public OXGoldVis(IGalleryService galleryService, OXGoldImagePipeline pipelineProvider)
        {
            this.galleryService = galleryService;
            this.pipelineProvider = pipelineProvider;
        }

        public ImageGeneratorMetaData GetMetaData()
        {
            return new ImageGeneratorMetaData(new[] { Albums.OX });
        }

        public Task Generate(IProgressBar progress)
        {
            return Task.CompletedTask;

            var maxDepth = 5;
            progress.SetMaxTicks(9);

            for (var i = 1; i <= maxDepth; i++)
            {
                Run(progress, i);
                progress.Tick();
            }

            return Task.CompletedTask;
        }

        private Task Run(IProgressBar progress, int depth)
        {
            var id = $"OX Gold {depth} sim.png";
            progress.SetMessage(id);

            var metaData = new GalleryItemMetadata
            {
                Id = id,
                FileName = id,
                Albums = new[] { Albums.OX },
                Description = "Desc...",
            };

            if (File.Exists(galleryService.GetImageFilename(metaData)))
                return Task.CompletedTask;

            var rendererConfig = new SquareTreeRendererOptions(3, 500)
            {
                MaxDepth = depth
            };

            var pipeline = pipelineProvider.Create(metaData, rendererConfig, x => x.GetWinsOverLoses());
            pipeline.Run(progress);

            return Task.CompletedTask;
        }
    }

}
