using Faze.Abstractions.GameStates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Faze.Examples.GridGames
{
    public class PiecesBoardState<TPlayer> : IGameState<int, int?, TPlayer>
    {
        private readonly TPlayer player;
        private readonly Func<int, int, IEnumerable<int>> getMoves;
        private List<int> pieces;
        private List<int> availableMoves;

        public PiecesBoardState(TPlayer player, int dimension, Func<int, int, IEnumerable<int>> getMoves)
            : this(player, dimension, getMoves, new List<int>(), Enumerable.Range(0, dimension * dimension))
        {
        }

        private PiecesBoardState(TPlayer player, int dimension, Func<int, int, IEnumerable<int>> getMoves, List<int> pieces, IEnumerable<int> availableMoves)
        {
            this.player = player;
            this.Dimension = dimension;
            this.getMoves = getMoves;
            this.pieces = pieces.ToList();
            this.availableMoves = availableMoves.ToList();
        }

        public int Dimension { get; }
        public int TotalPlayers => 1;
        public TPlayer GetCurrentPlayer() => player;

        public IEnumerable<int> GetAvailableMoves()
        {
            return availableMoves;
        }

        public IGameState<int, int?, TPlayer> Move(int move)
        {
            if (!availableMoves.Contains(move))
                throw new InvalidDataException($"Move {move} has already been made");

            var influence = getMoves(move, Dimension).ToList();
            var newAvailableMoves = availableMoves.Except(influence).ToList();
            var newPieces = pieces.Concat(new[] { move }).ToList();

            return new PiecesBoardState<TPlayer>(player, Dimension, getMoves, newPieces, availableMoves);
        }

        public int? GetResult()
        {
            return !availableMoves.Any()
                ? pieces.Count
                : (int?)null;
        }
    }

}
