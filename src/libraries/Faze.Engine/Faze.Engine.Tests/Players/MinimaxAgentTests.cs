using Faze.Engine.Players;
using Faze.Utilities.Testing;
using Faze.Abstractions.Players;
using Xunit;
using Faze.Abstractions.Core;
using Faze.Core.TreeLinq;
using Faze.Core.IO;
using Faze.Core.Serialisers;

namespace Faze.Engine.Tests.Players
{
    public class MinimaxAgentTests
    {
        private readonly ITreeDataProvider<int?> treeDataProvider;
        private readonly TestGameStateService testGameStateService;

        public MinimaxAgentTests()
        {
            this.testGameStateService = new TestGameStateService();
            this.treeDataProvider = new TestFileTreeDataProvider(@"../../../Resources");
        }

        [Fact]
        public void CanFindBestMove1()
        {
            var agent = GetAgent();
            var gameTree = TreeFileExtensions.Load("MinimaxAgentTests/Tree1.json", treeDataProvider);
            var state = testGameStateService.CreateState(gameTree);

            var moves = agent.GetMoves(state);
            var move = moves.GetMove(0);
        }

        private IPlayer GetAgent()
        {
            return new MinimaxAgent();
        }
    }
}
