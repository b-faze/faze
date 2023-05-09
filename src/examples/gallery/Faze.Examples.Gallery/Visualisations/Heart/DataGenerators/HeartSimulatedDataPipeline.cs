using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Core.Adapters;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Games.Heart;
using Faze.Examples.Games.OX;

namespace Faze.Examples.Gallery.Visualisations.Heart
{
    public class HeartSimulatedDataPipeline
    {
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public HeartSimulatedDataPipeline(IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
        }

        public IPipeline Create(string dataId, int simulations)
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            var resultsMapper = new WinLoseDrawResultsTreeMapper(new GameSimulator(), simulations);

            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(dataId, treeDataProvider)
                .Map(resultsMapper)
                .GameTree(new SquareTreeAdapter(2))
                .Build(() => HeartState.Initial(p1Hand, p2Hand));

            return pipeline;
        }
    }
}
