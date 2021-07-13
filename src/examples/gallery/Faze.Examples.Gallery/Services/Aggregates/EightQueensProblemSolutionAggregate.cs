using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Services.Aggregates
{
    public class EightQueensProblemSolutionAggregate : IResultAggregate<EightQueensProblemSolutionAggregate>
    {
        private const int WinningScore = 8;

        public EightQueensProblemSolutionAggregate()
        {
        }

        public EightQueensProblemSolutionAggregate(SingleScoreResult result)
        {
            Add(result);
        }

        public EightQueensProblemSolutionAggregate(uint wins, uint loses)
        {
            Wins = wins;
            Loses = loses;
        }

        public uint Wins { get; private set; }
        public uint Loses { get; private set; }

        public EightQueensProblemSolutionAggregate Value => this;

        public void Add(SingleScoreResult result)
        {
            if (result == WinningScore)
            {
                Wins++;
            }
            else
            {
                Loses++;
            }
        }

        public void AddRange(IEnumerable<SingleScoreResult> results)
        {
            foreach (var result in results)
            {
                Add(result);
            }
        }

        public void Add(EightQueensProblemSolutionAggregate result)
        {
            Wins += result.Wins;
            Loses += result.Loses;
        }

        public void AddRange(IEnumerable<EightQueensProblemSolutionAggregate> results)
        {
            foreach (var result in results)
            {
                Add(result);
            }
        }

        public double GetWinsOverLoses()
        {
            return (double)Wins / Math.Max(1, Wins + Loses);
        }
    }
}
