using Shouldly;
using System;
using System.Collections.Generic;

namespace Faze.Utilities.Testing
{
    public class GameStateTestingService<TMove, TResult>
    {
        private readonly GameStateTestingServiceConfig<TMove, TResult> config;

        public GameStateTestingService(GameStateTestingServiceConfig<TMove, TResult> config)
        {
            this.config = config;
        }

        public void TestInitialAvailableMoves(IEnumerable<TMove> expectedMoves)
        {
            var state = config.StateFactory();

            state.GetAvailableMoves().ShouldBe(expectedMoves);
        }

        /// <summary>
        /// Runs through moves and checks no result until the end, results match and no available moves after result.
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="expectedResult"></param>
        public void TestMovesForResult(IEnumerable<TMove> moves, TResult expectedResult) 
        {
            var state = config.StateFactory();
            var path = new List<TMove>();

            foreach (var move in moves)
            {
                path.Add(move);

                var result = state.GetResult();
                result.ShouldBe(default, $"Result should be null for move sequence [{string.Join(",", path)}]");

                state.GetAvailableMoves().ShouldContain(move, $"Move '{move}' is unavailable for sequence [{string.Join(",", path)}]");
                state = state.Move(move);
            }

            state.GetResult().ShouldBe(expectedResult);
            state.GetAvailableMoves().ShouldBeEmpty();
        }
    }
}
