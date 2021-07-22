using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Examples.Gallery.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Visualisations.OX3D.DataGenerators
{
    public class OX3DDataGenerator6 : IDataGenerator
    {
        public const string Id = "OX3D_depth6_sim100";
        private readonly OX3DSimulatedDataPipeline pipelineProvider;

        public OX3DDataGenerator6(OX3DSimulatedDataPipeline pipelineProvider)
        {
            this.pipelineProvider = pipelineProvider;
        }

        string IDataGenerator.Id => Id;

        public Task Generate(IProgressTracker progress)
        {
            progress.SetMessage(Id);
            pipelineProvider.Create(Id, 100, 6).Run(progress);

            return Task.CompletedTask;
        }
    }
}
