using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Examples.Games.Sudoku;
using System.Collections.Generic;

namespace Faze.Examples.Gallery.Visualisations.Sudoku
{
    public class SudokuStateWrapper : IGameState<GridMove, WinLoseDrawResult?>
    {
        private readonly SudokuState state;
        private readonly GridMove? indexMove;

        public SudokuStateWrapper(SudokuState state) : this(state, null)
        {
        }

        private SudokuStateWrapper(SudokuState state, GridMove? indexMove)
        {
            this.state = state;
            this.indexMove = indexMove;
        }

        public bool HasPositionMove => indexMove.HasValue;

        public PlayerIndex CurrentPlayerIndex => state.CurrentPlayerIndex;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            if (indexMove.HasValue)
            {
                return state.GetAvailableMoveValues(indexMove.Value);
            }

            return state.GetAvailableMovePositions();
        }

        public WinLoseDrawResult? GetResult() => state.GetResult();

        public SudokuStateWrapper Move(GridMove move) 
        {
            if (indexMove.HasValue)
            {
                var complexMove = new SudokuMove(indexMove.Value, move);
                var newState = (SudokuState)state.Move(complexMove);
                return new SudokuStateWrapper(newState);
            }

            return new SudokuStateWrapper(state, move);
        }

        IGameState<GridMove, WinLoseDrawResult?> IGameState<GridMove, WinLoseDrawResult?>.Move(GridMove move)
        {
            return Move(move);
        }
    }
}
