using Faze.Abstractions;

namespace Faze.Abstractions
{
    public interface IGameSimulator
    {
        TResult Simulate<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state);

        void Simulate<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state, IResultAggregate<TResult> results);
    }
}