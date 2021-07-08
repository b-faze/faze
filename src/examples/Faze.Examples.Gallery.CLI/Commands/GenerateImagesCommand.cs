using CommandLine;
using MediatR;
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
        public Task<int> Handle(GenerateImagesCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
