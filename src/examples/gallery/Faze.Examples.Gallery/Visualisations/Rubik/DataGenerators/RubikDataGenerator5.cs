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
    public class RubikDataGenerator5 : IDataGenerator
    {
        public const string Id = "Rubik_depth5_sim5";
        private readonly RubikSimulatedDataPipeline pipelineProvider;

        public RubikDataGenerator5(RubikSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);
            // check 100 times if a solution is found by making 5 random moves
            pipelineProvider.Create(Id, dataDepth: 5, simulations: 100, simulationDepth: 5).Run(progress);

            return Task.CompletedTask;
        }
    }
}
