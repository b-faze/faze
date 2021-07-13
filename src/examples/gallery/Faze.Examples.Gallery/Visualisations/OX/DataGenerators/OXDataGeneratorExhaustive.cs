using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX.DataGenerators
{
    public class OXDataGeneratorExhaustive : IDataGenerator
    {
        public const string Id = "OX_exhaustive";
        private readonly OXSimulatedDataPipeline pipelineProvider;

        public OXDataGeneratorExhaustive(OXSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressBar progress)
        {
            progress.SetMessage(Id);
            pipelineProvider.Create(Id, 1, 9).Run(progress);

            return Task.CompletedTask;
        }
    }
}
