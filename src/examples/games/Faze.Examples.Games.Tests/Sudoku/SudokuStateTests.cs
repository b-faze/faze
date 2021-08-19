using Faze.Abstractions.GameResults;
using Faze.Examples.Games.Sudoku;
using Faze.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Examples.Games.Tests.Sudoku
{
    public class SudokuStateTests
    {
        private GameStateTestingService<SudokuMove, WinLoseDrawResult?> gameStateTestingService;

        public SudokuStateTests()
        {
            var gameStateTestingServiceConfig = new GameStateTestingServiceConfig<SudokuMove, WinLoseDrawResult?>(() => SudokuState.Initial());
            this.gameStateTestingService = new GameStateTestingService<SudokuMove, WinLoseDrawResult?>(gameStateTestingServiceConfig);
        }
    }
}
