using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Faze.Examples.GridGames
{
    public abstract class PiecesBoardState : IGameState<GridMove, SingleScoreResult?>
    {
        private List<GridMove> currentInfluence;
        private List<GridMove> availableMoves;
        private int currentScore;
        private bool fail;

        public PiecesBoardState(int dimension)
            : this(dimension, new GridMove[0], Enumerable.Range(0, dimension * dimension).Select(x => (GridMove)x), 0, false)
        {
        }

        protected PiecesBoardState(int dimension, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int currentScore, bool fail)
        {
            this.Dimension = dimension;
            this.currentInfluence = influence.ToList();
            this.availableMoves = availableMoves.ToList();
            this.currentScore = currentScore;
            this.fail = fail;
        }

        public int Dimension { get; }
        public int TotalPlayers => 1;
        public PlayerIndex CurrentPlayerIndex => 0;

        public IEnumerable<GridMove> GetAvailableMoves() => fail ? new GridMove[0].AsEnumerable() : availableMoves;

        public IGameState<GridMove, SingleScoreResult?> Move(GridMove move)
        {
            if (!availableMoves.Contains(move))
                throw new InvalidDataException($"Move {move} has already been made");

            if (currentInfluence.Contains(move))
                return Create(Dimension, currentInfluence, availableMoves, currentScore, fail: true);

            var moveInfluence = GetPieceMoves(move, Dimension);
            var newAvailableMoves = availableMoves.Except(new[] { move });

            var newInfluence = currentInfluence.Union(moveInfluence);
            var availableGoodMoves = newAvailableMoves.Except(newInfluence);
            if (!availableGoodMoves.Any())
                newAvailableMoves = availableGoodMoves;

            return Create(Dimension, newInfluence, newAvailableMoves, currentScore + 1, fail: false);
        }

        public SingleScoreResult? GetResult()
        {
            if (fail)
                return new SingleScoreResult(-1);

            return !availableMoves.Any()
                ? new SingleScoreResult(currentScore)
                : (SingleScoreResult?)null;
        }

        protected abstract IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension);
        protected abstract IGameState<GridMove, SingleScoreResult?> Create(int dimension, IEnumerable<GridMove> pieces, IEnumerable<GridMove> availableMoves, int score, bool fail);

    }

}
