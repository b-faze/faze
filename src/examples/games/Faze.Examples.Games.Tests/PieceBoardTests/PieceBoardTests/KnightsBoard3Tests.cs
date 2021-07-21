using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.GridGames.Pieces;
using Faze.Utilities.Testing;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.GridGames.Tests
{
    public class KnightsBoard3Tests
    {
        private const int Dimension = 3;
        private GameStateTestingService<GridMove, SingleScoreResult?> gameStateTestingService;

        public KnightsBoard3Tests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, SingleScoreResult?>(StateFactory);
            this.gameStateTestingService = new GameStateTestingService<GridMove, SingleScoreResult?>(gameStateTestingServiceConfig);
        }

        private IGameState<GridMove, SingleScoreResult?> StateFactory()
        {
            var config = new PiecesBoardStateConfig(Dimension, new KnightPiece());
            return new PiecesBoardState(config);
        }


        [Fact]
        public void CorrectInitialMoves()
        {
            var expected = Enumerable.Range(0, Dimension * Dimension).Select(i => new GridMove(i));

            gameStateTestingService.TestInitialAvailableMoves(expected);
        }

        [Fact]
        public void Game1()
        {
            var moves = new GridMove[] { 1, 7, 3, 4, 5 };
            var expectedResult = new SingleScoreResult(5);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game1Fail()
        {
            var moves = new GridMove[] { 1, 6 };
            var expectedResult = new SingleScoreResult(-1);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game2()
        {
            var moves = new GridMove[] { 4, 3, 5, 1, 7 };
            var expectedResult = new SingleScoreResult(5);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game2Fail()
        {
            var moves = new GridMove[] { 4, 3, 2 };
            var expectedResult = new SingleScoreResult(-1);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game2Fail2()
        {
            var moves = new GridMove[] { 4, 3, 5, 2 };
            var expectedResult = new SingleScoreResult(-1);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }
    }
}
