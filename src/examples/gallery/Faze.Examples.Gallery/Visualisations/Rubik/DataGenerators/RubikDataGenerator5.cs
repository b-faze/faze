using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.Rubik.DataGenerators
{
    public class RubikDataGeneratorNoSim5 : IDataGenerator
    {
        public const string Id = "Rubik_depth5_sim1";
        private readonly RubikSimulatedDataPipeline pipelineProvider;

        public RubikDataGeneratorNoSim5(RubikSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);
            pipelineProvider.Create(Id, 5, 1, 0).Run(progress);

            return Task.CompletedTask;
        }
    }
}
