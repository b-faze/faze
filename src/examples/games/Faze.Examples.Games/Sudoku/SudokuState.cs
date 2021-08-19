using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Examples.Games.Sudoku
{
    public class SudokuState : IGameState<SudokuMove, WinLoseDrawResult?>
    {
        private const int BoardSize = 81;
        private readonly SudokuStateConfig config;
        private byte?[] board;
        private WinLoseDrawResult? result;

        public static SudokuState Initial(SudokuStateConfig config = null)
        {
            return new SudokuState(config, new byte?[BoardSize], null);
        }

        private SudokuState(SudokuStateConfig config, byte?[] board, WinLoseDrawResult? result)
        {
            this.config = config;
            this.board = board;
            this.result = result;
        }

        public PlayerIndex CurrentPlayerIndex => PlayerIndex.P1;

        public IEnumerable<SudokuMove> GetAvailableMoves()
        {
            return board
                .Select((x, i) => new { x, i })
                .Where(p => p.x.HasValue)
                .SelectMany(p => Enumerable.Range(0, 9).Select(value => new SudokuMove(p.i, value)));
        }

        public WinLoseDrawResult? GetResult() => result;

        public IGameState<SudokuMove, WinLoseDrawResult?> Move(SudokuMove move)
        {
            var newBoard = new byte?[BoardSize];
            board.CopyTo(newBoard, 0);

            newBoard[move.Index] = (byte)move.Value;
            var newResult = CalculateResult(newBoard, move);

            return new SudokuState(config, newBoard, newResult);
        }

        private WinLoseDrawResult? CalculateResult(byte?[] board, SudokuMove move)
        {
            var dimention = 9;
            var horizontals = move.Index.GetRow(dimention);
            var verticals = move.Index.GetColumn(dimention);
            var boxes = move.Index.GetBox(dimention, 3);

            var isValid = Check(horizontals) && Check(verticals) && Check(boxes);

            if (!isValid)
                return WinLoseDrawResult.Lose;
            
            if (board.All(b => b.HasValue))
                return WinLoseDrawResult.Lose;

            return null;
        }

        private bool Check(IEnumerable<GridMove> moves)
        {
            var items = moves.Select(i => board[i])
                .Where(b => b.HasValue)
                .Select(b => b.Value);
            var seen = new HashSet<byte>();

            foreach (var item in items)
            {
                if (seen.Contains(item))
                    return false;

                seen.Add(item);
            }

            return true;
        }
    }
}
