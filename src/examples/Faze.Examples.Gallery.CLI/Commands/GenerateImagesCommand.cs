using CommandLine;
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
    }

    public class GenerateImagesCommandHandler : IRequestHandler<GenerateImagesCommand, int>
    {
        private readonly IEnumerable<IImageGenerator> generators;

        public GenerateImagesCommandHandler(IEnumerable<IImageGenerator> generators)
        {
            this.generators = generators;
        }

        public async Task<int> Handle(GenerateImagesCommand request, CancellationToken cancellationToken)
        {
            var dataGeneratorArr = generators.ToArray();

            for (var i = 0; i < dataGeneratorArr.Length; i++)
            {
                await dataGeneratorArr[i].Generate();
            }

            return 0;
        }
    }
}
