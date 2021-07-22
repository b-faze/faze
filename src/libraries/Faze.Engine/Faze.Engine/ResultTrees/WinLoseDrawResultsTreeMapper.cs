using Faze.Abstractions.Core;
using Faze.Abstractions.Engine;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Engine.ResultTrees
{
    public class WinLoseDrawResultsTreeMapper : ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate>
    {
        private readonly IGameSimulator engine;
        private readonly int simulations;

        public WinLoseDrawResultsTreeMapper(IGameSimulator engine, int leafSimulations)
        {
            this.engine = engine;
            this.simulations = leafSimulations;
        }

        public Tree<WinLoseDrawResultAggregate> Map(Tree<IGameState<GridMove, WinLoseDrawResult?>> tree, IProgressTracker progress)
        {
            var leafCount = tree.GetLeaves().Count();
            progress.SetMaxTicks(leafCount);

            return tree.MapValueAgg((state, info) => GetResults(state, info, progress), () => new WinLoseDrawResultAggregate());
        }

        private WinLoseDrawResultAggregate GetResults(IGameState<GridMove, WinLoseDrawResult?> state, TreeMapInfo info, IProgressTracker progress)
        {
            progress.SetMessage(string.Join(",", info.GetPath()));

            var resultAggregate = new WinLoseDrawResultAggregate();
            for (var i = 0; i < simulations; i++)
            {
                var result = engine.Simulate(state);
                if (result != null)
                {
                    resultAggregate.Add(result.Value);
                }
            }

            progress.Tick();
            return resultAggregate;
        }
    }
}
