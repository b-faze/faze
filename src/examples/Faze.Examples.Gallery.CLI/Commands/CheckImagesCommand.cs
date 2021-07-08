using CommandLine;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Commands
{
    [Verb("check-images", HelpText = "Reproduces the visualisations in a separate location and compares them to the existing")]
    public class CheckImagesCommand : IRequest<int>
    {
    }

    public class CheckImagesCommandHandler : IRequestHandler<CheckImagesCommand, int>
    {
        public Task<int> Handle(CheckImagesCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
