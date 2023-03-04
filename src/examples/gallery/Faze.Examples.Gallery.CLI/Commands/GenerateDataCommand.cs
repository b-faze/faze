using CommandLine;
using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Commands
{
    [Verb("generate-data", HelpText = "Produces the core tree data for the visualisations")]
    public class GenerateDataCommand : IRequest<int>
    {
        [Option("id", HelpText = "Generates data only for matching id")]
        public string Id { get; set; }
    }

    public class GenerateDataCommandHandler : IRequestHandler<GenerateDataCommand, int>
    {
        private readonly IProgressManager progressManager;
        private readonly IEnumerable<IDataGenerator> generators;
        private readonly IGalleryService galleryService;

        public GenerateDataCommandHandler(IProgressManager progressManager, IGalleryService galleryService, IEnumerable<IDataGenerator> dataGenerators)
        {
            this.progressManager = progressManager;
            this.galleryService = galleryService;
            this.generators = dataGenerators;
        }

        public async Task<int> Handle(GenerateDataCommand request, CancellationToken cancellationToken)
        {
            var dataGeneratorArr = GetGenerators(request).ToArray();
            using var outerProgress = progressManager.Start(dataGeneratorArr.Length, "generate-data");

            for (var i = 0; i < dataGeneratorArr.Length; i++)
            {
                await dataGeneratorArr[i].Generate(outerProgress.Spawn());
                outerProgress.Tick();
            }

            return 0;
        }

        private IEnumerable<IDataGenerator> GetGenerators(GenerateDataCommand request)
        {
            var newGenerators = generators.Where(g =>
            {
                return !File.Exists(galleryService.GetDataFilename(g.Id));
            });

            if (string.IsNullOrEmpty(request.Id))
                return newGenerators;

            return newGenerators.Where(x => x.Id == request.Id);
        }
    }
}
