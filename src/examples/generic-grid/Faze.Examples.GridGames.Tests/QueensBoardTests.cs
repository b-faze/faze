using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.GridGames.Pieces;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.GridGames.Tests
{
    public class QueensBoardTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void CorrectInitialMoveCount(int dimension)
        {
            var expectedInitialMoves = Enumerable.Range(0, dimension * dimension).Select(x => new GridMove(x));
            var state = new PiecesBoardState(new PiecesBoardStateConfig(dimension, new QueenPiece()));

            state.GetAvailableMoves().ShouldBe(expectedInitialMoves);
        }

        [Fact]
        public void CanFindSolutionToEightQueensProblem()
        {
            var moveSequence = new GridMove[] { 5, 11, 22, 24, 39, 41, 52, 58 };
            IGameState<GridMove, SingleScoreResult?> state = new PiecesBoardState(new PiecesBoardStateConfig(8, new QueenPiece()));

            foreach (var move in moveSequence)
            {
                state.GetResult().ShouldBeNull();
                state = state.Move(move);
            }

            var result = state.GetResult();
           
            result.ShouldNotBeNull();
            int value = result.Value;
            value.ShouldBe(8);
        }

        [Fact]
        public void CanFindIncorrectSolutionToEightQueensProblem()
        {
            var moveSequence = new GridMove[] { 5, 11, 22, 24, 39, 41, 52, 57 };
            IGameState<GridMove, SingleScoreResult?> state = new PiecesBoardState(new PiecesBoardStateConfig(8, new QueenPiece()));

            foreach (var move in moveSequence)
            {
                state.GetResult().ShouldBeNull();
                state = state.Move(move);
            }

            var result = state.GetResult();

            result.ShouldNotBeNull();
            int value = result.Value;
            value.ShouldBe(-1);
        }
    }
}
