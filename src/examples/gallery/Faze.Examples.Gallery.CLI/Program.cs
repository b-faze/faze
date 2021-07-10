using CommandLine;
using Faze.Examples.Gallery.CLI.Commands;
using Faze.Examples.Gallery.CLI.Utilities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var serviceProvider = ServiceProviderBuilder.Build();
            var mediator = serviceProvider.GetService<IMediator>();

            return await Parser.Default.ParseArguments<GenerateDataCommand, GenerateImagesCommand>(args)
                    .MapResult(
                        (GenerateDataCommand o) => mediator.Send(o), 
                        (GenerateImagesCommand o) => mediator.Send(o), 
                        (CheckImagesCommand o) => mediator.Send(o), 
                        err => Task.FromResult(1));
        }


    }
}
