﻿using Faze.Abstractions.Engine;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Engine.Simulators
{
    public class GameSimulator : IGameSimulator
    {
        private readonly Random rnd;

        public GameSimulator(Random rnd = null)
        {
            this.rnd = rnd ?? ThreadSafeRandom.Random();
        }

        public TResult Simulate<TMove, TResult>(IGameState<TMove, TResult> state, IPlayer[] players)
        {
            TResult result;
            while ((result = state.GetResult()) == null)
            {
                var move = GetMove(state, players);
                state = state.Move(move);
            }

            return result;
        }

        public TMove[] SimulatePath<TMove, TResult>(IGameState<TMove, TResult> state, IPlayer[] players, int maxDepth)
        {
            var path = new List<TMove>();
            var depth = 0;
            while (maxDepth > depth && state.GetResult() == null)
            {
                var move = GetMove(state, players);

                state = state.Move(move);
                path.Add(move);
                depth++;
            }

            return path.ToArray();
        }

        public IEnumerable<TResult> SampleResults<TMove, TResult>(IGameState<TMove, TResult> state, IPlayer[] players, int n)
        {
            while (n-- > 0)
            {
                yield return Simulate(state, players);
            }
        }

        public IEnumerable<TMove[]> SamplePaths<TMove, TResult>(IGameState<TMove, TResult> state, IPlayer[] players, int n, int maxDepth)
        {
            while (n-- > 0)
            {
                yield return SimulatePath(state, players, maxDepth);
            }
        }

        private TMove GetMove<TMove, TResult>(IGameState<TMove, TResult> state, IPlayer[] players)
        {
            var currentPlayer = players[state.CurrentPlayerIndex];
            var possibleMoves = currentPlayer.GetMoves(state);

            return possibleMoves.GetMove(rnd);
        }
    }
}
