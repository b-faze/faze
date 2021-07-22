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

namespace Faze.Examples.Games.Tests.OXnTests
{
    /// <summary>
    /// Equivilant to OXState for n = 3
    /// </summary>
    public class OXnState3Tests
    {
        private GameStateTestingService<GridMove, WinLoseDrawResult?> gameStateTestingService;

        public OXnState3Tests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, WinLoseDrawResult?>(() => new OXnState(3));
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
            var moves = new GridMove[] { 0, 3, 1, 4, 2 };
            var expectedResult = WinLoseDrawResult.Win;

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }

        [Fact]
        public void LosingGame1()
        {
            var moves = new GridMove[] { 0, 3, 1, 4, 7, 5 };
            var expectedResult = WinLoseDrawResult.Lose;

            gameStateTestingService.TestMovesForResult(moves, expectedResult);
        }
    }
}
