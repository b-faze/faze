using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Players;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using Xunit;

namespace Faze.Examples.OX.Tests
{
    public class OXStateGameTests
    {
        private GameStateTestingService<GridMove, WinLoseDrawResult?> gameStateTestingService;

        public OXStateGameTests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, WinLoseDrawResult?>(() => OXState.Initial);
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
