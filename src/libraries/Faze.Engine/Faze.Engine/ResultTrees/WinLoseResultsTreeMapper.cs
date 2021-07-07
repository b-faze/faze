using Faze.Abstractions.Core;
using Faze.Abstractions.Engine;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Core.TreeLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Engine.ResultTrees
{
    public class WinLoseResultsTreeMapper : IGameResultTreeMapper<WinLoseDrawResult?, WinLoseDrawResultAggregate>
    {
        private readonly IGameSimulator engine;

        public WinLoseResultsTreeMapper(IGameSimulator engine)
        {
            this.engine = engine;
        }

        private WinLoseDrawResultAggregate GetResults<TMove>(IGameState<TMove, WinLoseDrawResult?> state)
        {
            var simulations = 100;
            var resultAggregate = new WinLoseDrawResultAggregate();
            var results = engine
                .SampleResults(state, simulations)
                .Where(x => x != null)
                .Select(x => (WinLoseDrawResult)x);

            resultAggregate.AddRange(results);

            return resultAggregate;
        }

        public Tree<WinLoseDrawResultAggregate> GetResultsTree<TMove>(Tree<IGameState<TMove, WinLoseDrawResult?>> stateTree)
        {
            var resultsTree = stateTree
                            .MapTreeAgg(x => GetResults(x.Value));
            //.MapValue(x => x.Value.GetWinsOverLoses());

            return resultsTree;
        }
    }
}
