using Faze.Abstractions.GameMoves;

namespace Faze.Examples.Games.Sudoku
{
    public class SudokuMove
    {
        public SudokuMove(GridMove index, GridMove value)
        {
            Index = index;
            Value = value;
        }

        public GridMove Index { get; }
        public GridMove Value { get; }
    }
}
