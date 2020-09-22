using Faze.Abstractions.Engine;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Engine.Simulators
{
    public class GameSimulator : IGameSimulator
    {
        public TResult Simulate<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state)
        {
            TResult result;
            while ((result = state.Result) == null)
            {
                var move = state.CurrentPlayer.ChooseMove(state);
                state = state.Move(move);
            }

            return result;
        }

        public IEnumerable<TResult> Sample<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state, int n)
        {
            while (n-- > 0)
            {
                yield return Simulate(state);
            }
        }
    }
}
