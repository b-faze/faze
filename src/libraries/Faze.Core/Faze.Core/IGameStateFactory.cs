using Faze.Abstractions.GameStates;

namespace Faze.Core
{
    public interface IGameStateFactory<TMove, TResult>
    {
        IGameState<TMove, TResult> Create();
    }
}
