using System;
using System.Collections.Generic;

namespace Faze.Abstractions.GameResults
{
    public struct WinLoseDrawResultAggregate
    {
        public WinLoseDrawResultAggregate(uint wins, uint loses, uint draws)
        {
            Wins = wins;
            Loses = loses;
            Draws = draws;
        }

        public uint Wins { get; }
        public uint Loses { get; }
        public uint Draws { get; }

        public WinLoseDrawResultAggregate Add(WinLoseDrawResult result)
        {
            switch (result)
            {
                case WinLoseDrawResult.Win:
                    return new WinLoseDrawResultAggregate(Wins + 1, Loses, Draws);

                case WinLoseDrawResult.Lose:
                    return new WinLoseDrawResultAggregate(Wins, Loses + 1, Draws);

                case WinLoseDrawResult.Draw:
                    return new WinLoseDrawResultAggregate(Wins, Loses, Draws + 1);
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

        public WinLoseDrawResultAggregate Add(WinLoseDrawResultAggregate result)
        {
            return new WinLoseDrawResultAggregate(Wins + result.Wins, Loses + result.Loses, Draws + result.Draws);
        }
    }
}