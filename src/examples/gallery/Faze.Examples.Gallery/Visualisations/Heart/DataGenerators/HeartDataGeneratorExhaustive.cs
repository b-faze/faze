using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Interfaces;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartDataGeneratorExhaustive : IDataGenerator
    {
        public const string Id = "Heart_exhaustive";
        private readonly HeartSimulatedDataPipeline pipelineProvider;

        public HeartDataGeneratorExhaustive(HeartSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);
            pipelineProvider.Create(Id, 1).Run(progress);

            return Task.CompletedTask;
        }
    }
}
