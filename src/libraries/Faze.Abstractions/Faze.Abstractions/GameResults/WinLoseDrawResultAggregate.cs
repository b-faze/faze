using System;
using System.Collections.Generic;
using System.Drawing;

namespace Faze.Abstractions.GameResults
{
    public class WinLoseDrawResultAggregate : IResultAggregate<WinLoseDrawResultAggregate>
    {
        public WinLoseDrawResultAggregate()
        {
        }

        public WinLoseDrawResultAggregate(WinLoseDrawResult result)
        {
            Add(result);
        }

        public WinLoseDrawResultAggregate(uint wins, uint loses, uint draws)
        {
            Wins = wins;
            Loses = loses;
            Draws = draws;
        }

        public uint Wins { get; private set; }
        public uint Loses { get; private set; }
        public uint Draws { get; private set; }

        public WinLoseDrawResultAggregate Value => this;

        public void Add(WinLoseDrawResult result)
        {
            switch (result)
            {
                case WinLoseDrawResult.Win:
                    Wins++;
                    return;

                case WinLoseDrawResult.Lose:
                    Loses++;
                    return;

                case WinLoseDrawResult.Draw:
                    Draws++;
                    return;
            }

            throw new NotSupportedException($"Unknown result type '{result}'");
        }

        public void AddRange(IEnumerable<WinLoseDrawResult> results)
        {
            foreach (var result in results)
            {
                Add(result);
            }
        }

        public void Add(WinLoseDrawResultAggregate result)
        {
            Wins += result.Wins;
            Loses += result.Loses;
            Draws += result.Draws;
        }

        public void AddRange(IEnumerable<WinLoseDrawResultAggregate> results)
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

        public override bool Equals(object obj)
        {
            if (obj is WinLoseDrawResultAggregate agg)
            {
                return agg.Wins == Wins
                    && agg.Loses == Loses
                    && agg.Draws == Draws;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (Wins + Loses + Draws).GetHashCode();
        }

        public override string ToString()
        {
            return $"{Wins},{Loses},{Draws}";
        }
    }
}