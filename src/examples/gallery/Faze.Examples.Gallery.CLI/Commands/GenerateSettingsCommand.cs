using CommandLine;
using Faze.Examples.Gallery.Interfaces;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Commands
{
    [Verb("generate-settings", HelpText = "Produces the settings.json file needed for the gallery static site")]
    public class GenerateSettingsCommand : IRequest<int>
    {
        [Option("output", Required = true, HelpText = "Output file path")]
        public string OutputFilename { get; set; }
    }

    public class GenerateSettingsCommandHandler : IRequestHandler<GenerateSettingsCommand, int>
    {
        private readonly IEnumerable<IGalleryItemProvider> generators;
        private readonly IEnumerable<IVisualisationPipeline> visPipelines;

        public GenerateSettingsCommandHandler(IEnumerable<IGalleryItemProvider> generators, IEnumerable<IVisualisationPipeline> visPipelines)
        {
            this.generators = generators;
            this.visPipelines = visPipelines;
        }
        public async Task<int> Handle(GenerateSettingsCommand request, CancellationToken cancellationToken)
        {
            var allData = generators.SelectMany(g => g.GetMetaData());

            var settings = GetOrCreateSiteSettings(request);
            settings.ItemMetadata = allData.ToDictionary(x => x.FileId);
            settings.PipelineMetadata = GetPipelinedata().ToDictionary(x => x.Id);

            await SaveSiteSettings(request, settings);

            return 0;
        }

        private IEnumerable<GalleryPipelineMetadata> GetPipelinedata()
        {
            return visPipelines.Select(p => p.GetMetadata());
        }

        private GallerySiteSettings GetOrCreateSiteSettings(GenerateSettingsCommand request) 
        {
            var file = request.OutputFilename;
            if (File.Exists(file))
                return JsonConvert.DeserializeObject<GallerySiteSettings>(File.ReadAllText(file));

            return new GallerySiteSettings();
        }

        private Task SaveSiteSettings(GenerateSettingsCommand request, GallerySiteSettings settings)
        {
            var file = request.OutputFilename;
            var directory = Path.GetDirectoryName(file);

            if (!Directory.Exists(file))
                Directory.CreateDirectory(directory);

            return File.WriteAllTextAsync(file, JsonConvert.SerializeObject(settings, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            }));
        }
    }
}
