using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.OX;

namespace Faze.Examples.Gallery.CLI.Visualisations.OX.DataGenerators
{
    public class OXSimulatedDataPipeline
    {
        private readonly ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OXSimulatedDataPipeline(ITreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
        {
            this.treeDataProvider = treeDataProvider;
        }

        public IPipeline Create(string dataId, int simulations, int depth)
        {
            ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate> resultsMapper = new WinLoseDrawResultsTreeMapper(new GameSimulator(), simulations);

            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree(dataId, treeDataProvider)
                .Map(resultsMapper)
                .LimitDepth(depth)
                .GameTree(new OXStateTreeAdapter())
                .Build(() => OXState.Initial);

            return pipeline;
        }
    }
}
