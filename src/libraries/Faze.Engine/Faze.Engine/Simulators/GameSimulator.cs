﻿using Faze.Abstractions.Engine;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Players;
using Faze.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Engine.Simulators
{
    public class GameSimulator : IGameSimulator
    {
        private readonly IAgentManager agentProvider;
        private readonly Random rnd;

        public GameSimulator(IAgentManager agentProvider = null, Random rnd = null)
        {
            this.agentProvider = agentProvider ?? new MonkeyAgentManager();
            this.rnd = rnd ?? ThreadSafeRandom.Random();
        }

        public TResult Simulate<TMove, TResult>(IGameState<TMove, TResult> state)
        {
            TResult result;
            while ((result = state.GetResult()) == null)
            {
                var move = GetMove(state);
                state = state.Move(move);
            }

            return result;
        }        
        
        public TResult Simulate<TMove, TResult>(IGameState<TMove, TResult> state, int maxMoves)
        {
            TResult result;
            while ((result = state.GetResult()) == null && maxMoves > 0)
            {
                var move = GetMove(state);
                state = state.Move(move);
                maxMoves--;
            }

            return result;
        }

        public TMove[] SimulatePath<TMove, TResult>(IGameState<TMove, TResult> state, int maxDepth)
        {
            var path = new List<TMove>();
            var depth = 0;
            while (maxDepth > depth && state.GetResult() == null)
            {
                var move = GetMove(state);

                state = state.Move(move);
                path.Add(move);
                depth++;
            }

            return path.ToArray();
        }

        public IEnumerable<TResult> SampleResults<TMove, TResult>(IGameState<TMove, TResult> state, int n)
        {
            while (n-- > 0)
            {
                yield return Simulate(state);
            }
        }

        public IEnumerable<TMove[]> SamplePaths<TMove, TResult>(IGameState<TMove, TResult> state, int n, int maxDepth)
        {
            while (n-- > 0)
            {
                yield return SimulatePath(state, maxDepth);
            }
        }

        private TMove GetMove<TMove, TResult>(IGameState<TMove, TResult> state)
        {
            var currentPlayer = agentProvider.GetPlayer(state.CurrentPlayerIndex);
            var possibleMoves = currentPlayer.GetMoves(state);

            return possibleMoves.GetMove(rnd.NextDouble());
        }
    }
}
