using CommandLine;
using Faze.Abstractions.Core;
using Faze.Examples.Gallery.CLI.Interfaces;
using MediatR;
using System.Collections.Generic;
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
        private readonly IEnumerable<IImageGenerator> generators;

        public GenerateImagesCommandHandler(IProgressManager progressManager, IEnumerable<IImageGenerator> generators)
        {
            this.progressManager = progressManager;
            this.generators = generators;
        }

        public async Task<int> Handle(GenerateImagesCommand request, CancellationToken cancellationToken)
        {
            var dataGeneratorArr = GetGenerators(request).ToArray();
            using var outerProgress = progressManager.Start(dataGeneratorArr.Length, "generate-images");

            for (var i = 0; i < dataGeneratorArr.Length; i++)
            {
                await dataGeneratorArr[i].Generate(outerProgress.Spawn());
                outerProgress.Tick();
            }

            return 0;
        }

        private IEnumerable<IImageGenerator> GetGenerators(GenerateImagesCommand request) 
        {
            if (string.IsNullOrEmpty(request.Album))
                return generators;

            return generators.Where(g => g.GetMetaData().Albums?.Contains(request.Album) ?? false);
        }
    }
}
