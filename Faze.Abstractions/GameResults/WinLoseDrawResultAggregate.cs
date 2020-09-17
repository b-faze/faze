using System;

namespace Faze.Abstractions.GameResults
{
    public class WinLoseDrawResultAggregate : IWinLoseDrawResultAggregate, IResultAggregate<WinLoseDrawResult?>
    {
        public long Wins { get; private set; }
        public long Loses { get; private set; }
        public long Draws { get; private set; }
        public long Unknown { get; private set; }

        public static WinLoseDrawResultAggregate Empty => new WinLoseDrawResultAggregate();

        public void Add(WinLoseDrawResult? result)
        {
            switch (result)
            {
                case null:
                    Unknown++;
                    break;

                case WinLoseDrawResult.Win:
                    Wins++;
                    break;

                case WinLoseDrawResult.Lose:
                    Loses++;
                    break;

                case WinLoseDrawResult.Draw:
                    Draws++;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public void Add(IWinLoseDrawResultAggregate results)
        {
            Wins += results.Wins;
            Loses += results.Loses;
            Draws += results.Draws;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public IResultAggregate<IWinLoseDrawResultAggregate> Deserialise(string v)
        {
            throw new NotImplementedException();
        }

        IResultAggregate<WinLoseDrawResult?> IResultAggregate<WinLoseDrawResult?>.Deserialise(string v)
        {
            throw new NotImplementedException();
        }
    }
}