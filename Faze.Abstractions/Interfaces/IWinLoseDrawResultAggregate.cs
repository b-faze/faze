namespace Faze.Abstractions
{
    public interface IWinLoseDrawResultAggregate : IResultAggregate<IWinLoseDrawResultAggregate>
    {
        long Wins { get; }
        long Loses { get; }
        long Draws { get; }
    }
}