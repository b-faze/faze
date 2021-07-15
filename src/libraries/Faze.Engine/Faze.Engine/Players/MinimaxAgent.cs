using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Linq;

namespace Faze.Engine.Players
{
    public class MinimaxAgent<TResult> : IPlayer<TResult>
    {
        private readonly int foresight;
        private readonly IResultEvaluator<TResult> resultEvaluator;

        public MinimaxAgent(int foresight, IResultEvaluator<TResult> resultEvaluator)
        {
            this.foresight = foresight;
            this.resultEvaluator = resultEvaluator;
        }

        public IMoveDistribution<TMove> GetMoves<TMove>(IGameState<TMove, TResult> state)
        {
            if (foresight == 0)
            {
                return new MoveDistribution<TMove>(state.GetAvailableMoves());
            }

            var availableMoves = state.GetAvailableMoves().ToArray();
            if (availableMoves.Length == 0)
                return new MoveDistribution<TMove>();


            TMove bestMove = default;
            TResult bestScore = resultEvaluator.MinValue;
            var betterMoveFound = false;

            foreach (var move in availableMoves)
            {
                var score = GetScore(state.Move(move), foresight - 1, resultEvaluator.MinValue, resultEvaluator.MaxValue);
                if (resultEvaluator.Compare(score, bestScore) > 0)
                {
                    bestScore = score;
                    bestMove = move;
                    betterMoveFound = true;
                }
            }

            if (betterMoveFound)
            {
                return new MoveDistribution<TMove>(bestMove);
            }

            return new MoveDistribution<TMove>(availableMoves);

        }

        public TResult GetScore<TMove>(IGameState<TMove, TResult> state, int depth, TResult alpha, TResult beta)
        {
            var result = state.GetResult();
            var availableMoves = state.GetAvailableMoves().ToArray();
            if (depth == 0 || result != null || availableMoves.Length == 0)
            {
                return result;
                //return GetScore(result);
            }


            // alpha-beta pruning
            var isMaximiser = state.CurrentPlayerIndex == 0;
            var score = isMaximiser ? resultEvaluator.MinValue : resultEvaluator.MaxValue;

            foreach (var move in availableMoves)
            {
                var childScore = GetScore(state.Move(move), depth - 1, alpha, beta);
                if (isMaximiser)
                {
                    score = Max(score, childScore);
                    if (resultEvaluator.Compare(score, beta) > 0) 
                        break;
                    alpha = score;
                }
                else
                {
                    score = Min(score, childScore);
                    if (resultEvaluator.Compare(score, alpha) < 0) 
                        break;
                    beta = score;
                }
            }

            return score;
        }

        private TResult Max(TResult a, TResult b)
        {
            return resultEvaluator.Compare(a, b) >= 0 ? a : b;
        }

        private TResult Min(TResult a, TResult b)
        {
            return resultEvaluator.Compare(a, b) <= 0 ? a : b;
        }
    }
}
