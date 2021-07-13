using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX.DataGenerators
{
    public class OXDataGenerator5 : IDataGenerator
    {
        public const string Id = "OX_depth5_sim100";
        private readonly OXSimulatedDataPipeline pipelineProvider;

        public OXDataGenerator5(OXSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressBar progress)
        {
            progress.SetMessage(Id);
            pipelineProvider.Create(Id, 100, 5).Run(progress);

            return Task.CompletedTask;
        }
    }
}
