using System.Linq;

namespace Faze.Abstractions.GameResults
{
    public class SinglePlayerWinLoseAggregate : IResultAggregate<SinglePlayerWinLose?>
    {
        public long Wins { get; private set; }
        public long Losses { get; private set; }

        public void Add(SinglePlayerWinLose? result)
        {
            if (result == null)
                return;

            if (result == SinglePlayerWinLose.Win)
                Wins++;

            if (result == SinglePlayerWinLose.Lose)
                Losses++;
        }

        public void Add(SinglePlayerWinLoseAggregate result)
        {
            Wins += result.Wins;
            Losses += result.Losses;
        }

        public IResultAggregate<SinglePlayerWinLose?> Deserialise(string v)
        {
            var parts = v.Split(',').Select(long.Parse).ToArray();
            return new SinglePlayerWinLoseAggregate
            {
                Wins = parts[0],
                Losses = parts[1]
            };
        }

        public string Serialize()
        {
            return string.Join(",", new[] { Wins, Losses });
        }
    }
}