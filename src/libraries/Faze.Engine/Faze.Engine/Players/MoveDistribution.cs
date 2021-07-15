using Faze.Abstractions.Core;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Engine.Players
{
    public class MoveDistribution<TMove> : IMoveDistribution<TMove>
    {
        private readonly (TMove move, uint confidence)[] moveConfidence;
        private readonly uint totalConfidence;

        public MoveDistribution() : this(new TMove[0]) { }
        public MoveDistribution(TMove move) : this(new[] { move }) { }

        public MoveDistribution(IEnumerable<TMove> moves) : this(moves.Select(move => (move, (uint)1))) { }

        public MoveDistribution(IEnumerable<(TMove, uint confidence)> moveConfidence)
        {
            var sanitisedMoveConfidence = new List<(TMove move, uint confidence)>();
            var skippedItems = false;

            foreach (var item in moveConfidence)
            {
                if (item.confidence == 0)
                {
                    skippedItems = true;
                    continue;
                }

                sanitisedMoveConfidence.Add(item);
                totalConfidence += item.confidence;
            }

            if (sanitisedMoveConfidence.Count == 0 && skippedItems)
            {
                throw new Exception("Every move confidence cannot be zero");
            }
            
            this.moveConfidence = sanitisedMoveConfidence.ToArray();
        }

        public TMove GetMove(UnitInterval ui)
        {
            double targetConfidence = ui * totalConfidence;
            uint currentConfidence = 0;
            
            foreach (var move in moveConfidence)
            {
                currentConfidence += move.confidence;

                if (currentConfidence >= targetConfidence)
                    return move.move;
            }

            throw new Exception($"Unable to find a move within targetConfidence '{targetConfidence}'");
        }

        public bool IsEmpty() => totalConfidence == 0;
    }
}