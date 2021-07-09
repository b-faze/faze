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
        private int pieces;
        private List<GridMove> availableMoves;

        public PiecesBoardState(int dimension)
            : this(dimension, 0, Enumerable.Range(0, dimension * dimension).Select(x => (GridMove)x))
        {
        }

        protected PiecesBoardState(int dimension, int pieces, IEnumerable<GridMove> availableMoves)
        {
            this.Dimension = dimension;
            this.pieces = pieces;
            this.availableMoves = availableMoves.ToList();
        }

        public int Dimension { get; }
        public int TotalPlayers => 1;
        public PlayerIndex CurrentPlayerIndex => 0;

        public IEnumerable<GridMove> GetAvailableMoves() => availableMoves;

        public IGameState<GridMove, SingleScoreResult?> Move(GridMove move)
        {
            if (!availableMoves.Contains(move))
                throw new InvalidDataException($"Move {move} has already been made");

            var influence = GetPieceMoves(move, Dimension).Concat(new[] { move });
            var newAvailableMoves = availableMoves.Except(influence);

            return Create(Dimension, pieces + 1, newAvailableMoves);
        }

        public SingleScoreResult? GetResult()
        {
            return !availableMoves.Any()
                ? new SingleScoreResult(pieces)
                : (SingleScoreResult?)null;
        }

        protected abstract IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension);
        protected abstract IGameState<GridMove, SingleScoreResult?> Create(int dimension, int pieces, IEnumerable<GridMove> availableMoves);

    }

}
