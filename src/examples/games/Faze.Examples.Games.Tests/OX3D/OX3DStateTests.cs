using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Examples.Games.GridGames;
using Faze.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Examples.Games.Tests.OX3DTests
{
    public class OX3DStateTests
    {
        private GameStateTestingService<GridMove, WinLoseDrawResult?> gameStateTestingService;

        public OX3DStateTests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, WinLoseDrawResult?>(() => OX3DState.Initial());
            this.gameStateTestingService = new GameStateTestingService<GridMove, WinLoseDrawResult?>(gameStateTestingServiceConfig);
        }

        [Fact]
        public void CorrectInitialAvailableMoves()
        {
            var expected = new GridMove[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            gameStateTestingService.TestInitialAvailableMoves(expected);
        }

        [Fact]
        public void WinningGame1()
        {
            var moves = new GridMove[] { 0, 4, 8, 2, 6, 3, 5, 7, 1, 4, 4, 0, 8, 0, 8 };
            var expectedResult = WinLoseDrawResult.Win;

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void LosingGame1()
        {
            var moves = new GridMove[] { 0, 4, 8, 2, 6, 3, 5, 7, 1, 4, 4, 1, 8, 1 };
            var expectedResult = WinLoseDrawResult.Lose;

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void DrawingGame1()
        {
            // TODO
        }
    }
}
