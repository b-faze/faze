using CommandLine;
using Faze.Examples.Gallery.CLI.Commands;
using Faze.Examples.Gallery.CLI.Utilities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var serviceProvider = ServiceProviderBuilder.Build();
            var mediator = serviceProvider.GetService<IMediator>();

            int result = -1;

            try
            {
                result = await Parser.Default.ParseArguments<GenerateDataCommand, GenerateImagesCommand, GenerateSettingsCommand>(args)
                    .MapResult(
                        (GenerateDataCommand o) => mediator.Send(o),
                        (GenerateImagesCommand o) => mediator.Send(o),
                        (GenerateSettingsCommand o) => mediator.Send(o),
                        (CheckImagesCommand o) => mediator.Send(o),
                        err => Task.FromResult(1));

                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
            return result;
        }


    }
}
