
namespace Faze.Abstractions.GameResults
{
    public interface IMuliplayerGameResult<TResult>
    {
        TResult ResultFor<TPlayer>(TPlayer player);
    }
}
