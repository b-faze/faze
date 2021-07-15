using Faze.Engine.Players;
using Faze.Utilities.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Faze.Abstractions.Players;

namespace Faze.Engine.Tests.Players
{
    public class MonkeyAgentTests
    {
        private readonly IPlayer agent;
        private readonly TestGameStateService testGameStateService;

        public MonkeyAgentTests()
        {
            this.agent = new MonkeyAgent();
            this.testGameStateService = new TestGameStateService();
        }

        [Fact]
        public void CanHandleEmptyMoves()
        {
            var stateMoves = new int[0];
            var state = testGameStateService.CreateState<int, object>(stateMoves);
            var moves = agent.GetMoves(state);
        }

        [Fact]
        public void ReturnsUnitformMoveDistribution()
        {
            var stateMoves = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var state = testGameStateService.CreateState<int, object>(stateMoves);
            var moves = agent.GetMoves(state);

            for(var i = 0; i < stateMoves.Length; i++)
            {
                var expectedMove = stateMoves[i];
                var actualMove = moves.GetMove((double)i / stateMoves.Length);
                actualMove.ShouldBe(expectedMove);
            }
        }
    }
}
