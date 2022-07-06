using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Engine.ResultTrees;
using Faze.Engine.Simulators;
using Faze.Examples.Games.OX;
using Faze.Core.TreeLinq;
using Faze.Core.Adapters;
using Faze.Examples.Games.GridGames;

namespace Faze.Examples.Gallery.Visualisations.OX3D.DataGenerators
{
    public class OX3DSimulatedDataPipeline
    {
        private readonly IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider;

        public OX3DSimulatedDataPipeline(IFileTreeDataProvider<WinLoseDrawResultAggregate> treeDataProvider)
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
                .GameTree(new SquareTreeAdapter(3))
                .Build(() => OX3DState.Initial());

            return pipeline;
        }
    }
}
