using System.Linq;

namespace Faze.Abstractions.GameResults
{
    public class WinDrawResultAggregate<TPlayer> : IWinLoseDrawResultAggregate, IResultAggregate<WinDrawResult<TPlayer>>
    {
        private readonly TPlayer player;

        public WinDrawResultAggregate(TPlayer player)
        {
            this.player = player;
        }

        public long Wins { get; private set; }
        public long Loses { get; private set; }
        public long Draws { get; private set; }
        public long Unknown { get; private set; }

        public void Add(WinDrawResult<TPlayer> result)
        {
            if (result == null)
            {
                Unknown++;
                return;
            }

            var playerResult = result.ResultFor(player);
            switch (playerResult)
            {
                case WinLoseDrawResult.Win:
                    Wins++;
                    break;

                case WinLoseDrawResult.Lose:
                    Loses++;
                    break;

                case WinLoseDrawResult.Draw:
                    Draws++;
                    break;
            }
        }

        public void Add(IWinLoseDrawResultAggregate result)
        {
            Wins += result.Wins;
            Loses += result.Loses;
            Draws += result.Draws;
        }

        public IResultAggregate<IWinLoseDrawResultAggregate> Deserialise(string v)
        {
            var parts = v.Split(',').Select(long.Parse).ToArray();
            return new WinDrawResultAggregate<TPlayer>(player)
            {
                Wins = parts[0],
                Draws = parts[1],
                Loses = parts[2]
            };
        }

        public string Serialize()
        {
            return string.Join(",", new[] { Wins, Draws, Loses });
        }

        IResultAggregate<WinDrawResult<TPlayer>> IResultAggregate<WinDrawResult<TPlayer>>.Deserialise(string v)
        {
            return (IResultAggregate<WinDrawResult<TPlayer>>)Deserialise(v);
        }
    }
}