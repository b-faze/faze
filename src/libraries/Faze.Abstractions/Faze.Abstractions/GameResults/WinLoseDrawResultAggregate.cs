using System;
using System.Collections.Generic;

namespace Faze.Abstractions.GameResults
{
    public class WinLoseDrawResultAggregate
    {
        public WinLoseDrawResultAggregate()
        {
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
    }
}