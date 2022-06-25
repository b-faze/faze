using Faze.Abstractions.Core;
using Faze.Abstractions.Engine;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using Faze.Examples.Games.Rubik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Examples.Gallery.Visualisations.Rubik.DataGenerators
{
    public class RubikResultsTreeMapper : ITreeMapper<IGameState<GridMove, RubikResult?>, WinLoseDrawResultAggregate>
    {
        private readonly IGameSimulator engine;
        private readonly int simulations;
        private readonly int maxSimulationDepth;

        public RubikResultsTreeMapper(IGameSimulator engine, int leafSimulations, int maxSimulationDepth)
        {
            this.engine = engine;
            this.simulations = leafSimulations;
            this.maxSimulationDepth = maxSimulationDepth;
        }

        public Tree<WinLoseDrawResultAggregate> Map(Tree<IGameState<GridMove, RubikResult?>> tree, IProgressTracker progress)
        {
            var leafCount = tree.GetLeaves().Count();
            progress.SetMaxTicks(leafCount);

            return tree.MapValueAgg((state, info) => GetResults(state, info, progress), () => new WinLoseDrawResultAggregate());
        }

        private WinLoseDrawResultAggregate GetResults(IGameState<GridMove, RubikResult?> state, TreeMapInfo info, IProgressTracker progress)
        {
            progress.SetMessage(string.Join(",", info.GetPath()));

            var resultAggregate = new WinLoseDrawResultAggregate();
            for (var i = 0; i < simulations; i++)
            {
                var result = engine.Simulate(state, maxSimulationDepth);
                resultAggregate.Add(result.HasValue ? WinLoseDrawResult.Win : WinLoseDrawResult.Lose);
            }

            progress.Tick();
            return resultAggregate;
        }
    }
}
