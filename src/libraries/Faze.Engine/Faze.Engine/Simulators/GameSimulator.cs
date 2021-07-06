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
            while ((result = state.GetResult()) == null)
            {
                var move = state.GetCurrentPlayer().ChooseMove(state);
                state = state.Move(move);
            }

            return result;
        }

        public TMove[] SimulatePath<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state, int maxDepth)
        {
            var path = new List<TMove>();
            var depth = 0;
            while (maxDepth > depth && state.GetResult() == null)
            {
                var move = state.GetCurrentPlayer().ChooseMove(state);

                state = state.Move(move);
                path.Add(move);
                depth++;
            }

            return path.ToArray();
        }

        public IEnumerable<TResult> SampleResults<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state, int n)
        {
            while (n-- > 0)
            {
                yield return Simulate(state);
            }
        }

        public IEnumerable<TMove[]> SamplePaths<TMove, TResult>(IGameState<TMove, TResult, IPlayer> state, int n, int maxDepth)
        {
            while (n-- > 0)
            {
                yield return SimulatePath(state, maxDepth);
            }
        }
    }
}
