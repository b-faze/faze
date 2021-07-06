using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Abstractions.Players
{
    public class PossibleMoves<TMove>
    {
        private (TMove move, int confidence)[] moveConfidence;
        private int totalConfidence;

        public PossibleMoves(IEnumerable<(TMove, int confidence)> moveConfidence)
        {
            this.moveConfidence = moveConfidence.ToArray();
            this.totalConfidence = this.moveConfidence.Sum(x => x.confidence);
        }

        public TMove GetMove(Random rnd)
        {
            var targetConfidence = rnd.NextDouble() * totalConfidence;
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