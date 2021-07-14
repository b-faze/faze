using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.GridGames.Pieces;
using Faze.Examples.Testing;
using System;
using System.Linq;
using Xunit;

namespace Faze.Examples.GridGames.Tests
{
    public class RooksBoard3Tests
    {
        private const int Dimension = 3;
        private GameStateTestingService<GridMove, SingleScoreResult?> gameStateTestingService;

        public RooksBoard3Tests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, SingleScoreResult?>(StateFactory);
            this.gameStateTestingService = new GameStateTestingService<GridMove, SingleScoreResult?>(gameStateTestingServiceConfig);
        }

        private IGameState<GridMove, SingleScoreResult?> StateFactory()
        {
            var config = new PiecesBoardStateConfig(Dimension, new RookPiece());
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
            var moves = new GridMove[] { 0, 4, 8 };
            var expectedResult = new SingleScoreResult(3);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game1Fail()
        {
            var moves = new GridMove[] { 0, 2 };
            var expectedResult = new SingleScoreResult(-1);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game2()
        {
            var moves = new GridMove[] { 3, 1, 8 };
            var expectedResult = new SingleScoreResult(3);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void Game2Fail()
        {
            var moves = new GridMove[] { 3, 0 };
            var expectedResult = new SingleScoreResult(-1);

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }
    }
}
