using Faze.Abstractions.GameStates;
using System;

namespace Faze.Utilities.Testing
{
    public class GameStateTestingServiceConfig<TMove, TResult>
    {
        public GameStateTestingServiceConfig(Func<IGameState<TMove, TResult>> stateFactory)
        {
            StateFactory = stateFactory;
        }

        public Func<IGameState<TMove, TResult>> StateFactory { get; }
    }
}
