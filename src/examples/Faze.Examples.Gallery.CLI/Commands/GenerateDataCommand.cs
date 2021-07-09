using CommandLine;
using Faze.Abstractions.Core;
using Faze.Examples.Gallery.CLI.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Commands
{
    [Verb("generate-data", HelpText = "Produces the core tree data for the visualisations")]
    public class GenerateDataCommand : IRequest<int>
    {
    }

    public class GenerateDataCommandHandler : IRequestHandler<GenerateDataCommand, int>
    {
        private readonly IProgressManager progressManager;
        private readonly IEnumerable<IDataGenerator> dataGenerators;

        public GenerateDataCommandHandler(IProgressManager progressManager, IEnumerable<IDataGenerator> dataGenerators)
        {
            this.progressManager = progressManager;
            this.dataGenerators = dataGenerators;
        }

        public async Task<int> Handle(GenerateDataCommand request, CancellationToken cancellationToken)
        {
            var dataGeneratorArr = dataGenerators.ToArray();
            using var outerProgress = progressManager.Start(dataGeneratorArr.Length, "generate-data");

            for (var i = 0; i < dataGeneratorArr.Length; i++)
            {
                await dataGeneratorArr[i].Generate(outerProgress.Spawn());
                outerProgress.Tick();
            }

            return 0;
        }
    }
}
