using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Examples.Games.Rubik;
using Faze.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Examples.Games.Tests.Rubik
{
    public class SolvedRubikStateTests
    {
        private GameStateTestingService<GridMove, RubikResult?> gameStateTestingService;

        public SolvedRubikStateTests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<GridMove, RubikResult?>(() => RubikState.InitialSolved);
            this.gameStateTestingService = new GameStateTestingService<GridMove, RubikResult?>(gameStateTestingServiceConfig);
        }

        [Fact]
        public void CorrectInitialAvailableMoves()
        {
            var expected = new GridMove[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14 };

            gameStateTestingService.TestInitialAvailableMoves(expected);
        }
    }
}
