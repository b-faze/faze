using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.Games.GridGames.Pieces;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.Games.GridGames.Tests
{
    public class EightQueensProblemTests
    {
        private GameStateTestingService<GridMove, SingleScoreResult?> gameStateTestingService;

        public EightQueensProblemTests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, SingleScoreResult?>(StateFactory);
            this.gameStateTestingService = new GameStateTestingService<GridMove, SingleScoreResult?>(gameStateTestingServiceConfig);
        }

        private IGameState<GridMove, SingleScoreResult?> StateFactory()
        {
            var config = new PiecesBoardStateConfig(8, new QueenPiece(), onlySafeMoves: true);
            return new PiecesBoardState(config);
        }


        [Fact]
        public void CorrectInitialMoves()
        {
            var expected = Enumerable.Range(0, 64).Select(i => new GridMove(i));
            gameStateTestingService.TestInitialAvailableMoves(expected);
        }

        [Fact]
        public void CanFindSolutionToEightQueensProblem()
        {
            var moves = new GridMove[] { 5, 11, 22, 24, 39, 41, 52, 58 };
            gameStateTestingService.TestMovesForResult(moves, new SingleScoreResult(8));
        }

        [Fact]
        public void CanFindIncorrectSolutionToEightQueensProblem()
        {
            var moves = new GridMove[] { 5, 11, 22, 24, 39, 44, 49 };
            gameStateTestingService.TestMovesForResult(moves, new SingleScoreResult(7));
        }
    }
}
