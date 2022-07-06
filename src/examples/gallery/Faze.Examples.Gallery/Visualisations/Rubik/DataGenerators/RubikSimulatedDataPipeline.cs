using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Games.Rubik;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Rubik.DataGenerators
{
    public class RubikSimulatedDataPipeline
    {
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public RubikSimulatedDataPipeline(IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
        }

        public IPipeline Create(string dataId, int dataDepth, int simulations, int simulationDepth)
        {
            var resultsMapper = new RubikResultsTreeMapper(new GameSimulator(), simulations, simulationDepth);

            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(dataId, treeDataProvider)
                .Map(resultsMapper)
                .LimitDepth(dataDepth)
                .GameTree(new SquareTreeAdapter(4))
                .Build(() => RubikState.InitialSolved);

            return pipeline;
        }
    }
}
