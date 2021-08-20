using Faze.Abstractions.Core;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuTreeAdapter : ITreeAdapter<SudokuStateWrapper>
    {
        public IEnumerable<SudokuStateWrapper> GetChildren(SudokuStateWrapper state)
        {
            if (state.HasPositionMove)
            {
                return MapMoves(state, 9);
            }

            return MapMoves(state, 81);
        }

        private SudokuStateWrapper[] MapMoves(SudokuStateWrapper state, int size)
        {
            var children = new SudokuStateWrapper[size];
            foreach (var move in state.GetAvailableMoves())
            {
                children[move] = state.Move(move);
            }
            return children;
        }
    }
}
