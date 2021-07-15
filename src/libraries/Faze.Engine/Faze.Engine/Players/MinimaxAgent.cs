using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;

namespace Faze.Engine.Players
{
    public class MinimaxAgent : IPlayer
    {
        public IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state)
        {
            throw new NotImplementedException();
        }
    }
}
