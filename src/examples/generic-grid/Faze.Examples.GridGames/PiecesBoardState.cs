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
        private List<GridMove> pieces;
        private List<GridMove> availableMoves;

        public PiecesBoardState(int dimension)
            : this(dimension, new List<GridMove>(), Enumerable.Range(0, dimension * dimension).Select(x => (GridMove)x))
        {
        }

        protected PiecesBoardState(int dimension, List<GridMove> pieces, IEnumerable<GridMove> availableMoves)
        {
            this.Dimension = dimension;
            this.pieces = pieces.ToList();
            this.availableMoves = availableMoves.ToList();
        }

        public int Dimension { get; }
        public int TotalPlayers => 1;
        public PlayerIndex CurrentPlayerIndex => 0;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            return availableMoves;
        }

        public IGameState<GridMove, SingleScoreResult?> Move(GridMove move)
        {
            if (!availableMoves.Contains(move))
                throw new InvalidDataException($"Move {move} has already been made");

            var influence = GetPieceMoves(move, Dimension).ToList();
            var newAvailableMoves = availableMoves.Except(influence).ToList();
            var newPieces = pieces.Concat(new[] { move }).ToList();

            return Create(Dimension, newPieces, newAvailableMoves);
        }

        public SingleScoreResult? GetResult()
        {
            return !availableMoves.Any()
                ? new SingleScoreResult(pieces.Count)
                : (SingleScoreResult?)null;
        }

        protected abstract IEnumerable<GridMove> GetPieceMoves(int posIndex, int dimension);
        protected abstract IGameState<GridMove, SingleScoreResult?> Create(int dimension, List<GridMove> pieces, IEnumerable<GridMove> availableMoves);

    }

}
