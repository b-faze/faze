using Faze.Abstractions.Core;
using Faze.Abstractions.GameStates;

namespace Faze.Abstractions.Engine
{
    public interface IGameResultTreeMapper<TResultIn, TResult>
    {
        Tree<TResult> GetResultsTree<TMove>(Tree<IGameState<TMove, TResultIn>> state);
    }
}