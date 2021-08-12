using CommandLine;
using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Commands
{
    [Verb("generate-images", HelpText = "Produces the visualisations from the pre-computed data")]
    public class GenerateImagesCommand : IRequest<int>
    {
        [Option("album", HelpText = "only generate images from album")]
        public string Album { get; set; }
    }

    public class GenerateImagesCommandHandler : IRequestHandler<GenerateImagesCommand, int>
    {
        private readonly IProgressManager progressManager;
        private readonly IEnumerable<IImageGenerator2> generators;
        private readonly IPipelineProvider pipelineProvider;
        private readonly IGalleryService galleryService;

        public GenerateImagesCommandHandler(IProgressManager progressManager, IEnumerable<IImageGenerator2> generators, IPipelineProvider pipelineProvider, IGalleryService galleryService)
        {
            this.progressManager = progressManager;
            this.generators = generators;
            this.pipelineProvider = pipelineProvider;
            this.galleryService = galleryService;
        }

        public async Task<int> Handle(GenerateImagesCommand request, CancellationToken cancellationToken)
        {
            var allData = GetMetaData(request);
            using var outerProgress = progressManager.Start(allData.Count(), "generate-images");

            foreach (var item in allData)
            {
                await Generate(item, outerProgress.Spawn());
                outerProgress.Tick();
            }

            return 0;
        }

        private IEnumerable<GalleryItemMetadata> GetMetaData(GenerateImagesCommand request) 
        {
            var allMetaData = generators.SelectMany(g => g.GetMetaData());

            if (string.IsNullOrEmpty(request.Album))
                return allMetaData;

            return allMetaData.Where(x => x.Album?.Contains(request.Album) ?? false);
        }

        private Task Generate(GalleryItemMetadata item, IProgressTracker progress)
        {
            if (File.Exists(galleryService.GetImageFilename(item)))
                return Task.CompletedTask;

            var visPipeline = pipelineProvider.GetPipeline(item.PipelineId);

            var pipeline = visPipeline.Create(item);

            pipeline.Run(progress);

            return Task.CompletedTask;
        }
    }
}
