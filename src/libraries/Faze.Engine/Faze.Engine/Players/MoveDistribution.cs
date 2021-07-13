using Faze.Abstractions.Core;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Engine.Players
{
    public class MoveDistribution<TMove> : IMoveDistribution<TMove>
    {
        private (TMove move, int confidence)[] moveConfidence;
        private int totalConfidence;

        public MoveDistribution(IEnumerable<(TMove, int confidence)> moveConfidence)
        {
            this.moveConfidence = moveConfidence.ToArray();
            this.totalConfidence = this.moveConfidence.Sum(x => x.confidence);
        }

        public TMove GetMove(UnitInterval ui)
        {
            var targetConfidence = ui * totalConfidence;
            var currentConfidence = 0;
            
            foreach (var move in moveConfidence)
            {
                currentConfidence += move.confidence;

                if (currentConfidence > targetConfidence)
                    return move.move;
            }

            throw new NotSupportedException($"Unable to find a move within targetConfidence '{targetConfidence}'");
        }
    }
}