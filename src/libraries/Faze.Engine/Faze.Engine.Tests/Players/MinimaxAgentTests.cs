using Faze.Engine.Players;
using Faze.Utilities.Testing;
using Faze.Abstractions.Players;
using Xunit;
using Faze.Abstractions.Core;
using Faze.Core.TreeLinq;
using Faze.Core.IO;
using Faze.Core.Serialisers;
using System;
using Shouldly;

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
            var foresight = 1;
            var agent = GetAgent(foresight);
            var gameTree = TreeFileExtensions.Load("MinimaxAgentTests/tree1_depth1.json", treeDataProvider);
            var state = testGameStateService.CreateState(gameTree);

            var moves = agent.GetMoves(state);
            moves.GetMove(0).ShouldBe(1);
            moves.GetMove(1).ShouldBe(1);
        }

        [Fact]
        public void CanFindBestMove2()
        {
            var foresight = 3;
            var agent = GetAgent(foresight);
            var gameTree = TreeFileExtensions.Load("MinimaxAgentTests/tree2_depth2.json", treeDataProvider);
            var state = testGameStateService.CreateState(gameTree);

            var moves = agent.GetMoves(state);
            moves.GetMove(0).ShouldBe(0);
            moves.GetMove(1).ShouldBe(0);
        }

        [Fact]
        public void ReturnsUniformDistributionWhenLackingForesight()
        {
            var foresight = 2;
            var agent = GetAgent(foresight);
            var gameTree = TreeFileExtensions.Load("MinimaxAgentTests/tree2_depth2.json", treeDataProvider);
            var state = testGameStateService.CreateState(gameTree);

            var moves = agent.GetMoves(state);
            moves.GetMove(0).ShouldBe(0);
            moves.GetMove(1).ShouldBe(1);
        }

        private IPlayer<int?> GetAgent(int foresight)
        {
            var resultEvaluator = new TestResultEvaluator();
            return new MinimaxAgent<int?>(foresight, resultEvaluator);
        }
    }
}
