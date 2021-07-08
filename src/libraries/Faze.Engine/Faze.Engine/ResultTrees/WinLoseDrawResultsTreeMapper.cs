﻿using Faze.Abstractions.Core;
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

        public WinLoseDrawResultsTreeMapper(IGameSimulator engine, int simulations)
        {
            this.engine = engine;
            this.simulations = simulations;
        }

        public Tree<WinLoseDrawResultAggregate> Map(Tree<IGameState<GridMove, WinLoseDrawResult?>> tree)
        {
            return tree.MapTreeAgg(GetResults);
        }

        private WinLoseDrawResultAggregate GetResults(IGameState<GridMove, WinLoseDrawResult?> state)
        {
            var resultAggregate = new WinLoseDrawResultAggregate();
            for (var i = 0; i < simulations; i++)
            {
                var result = engine.Simulate(state);
                if (result != null)
                {
                    resultAggregate.Add(result.Value);
                }
            }


            return resultAggregate;
        }
    }
}