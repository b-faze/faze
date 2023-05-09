using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Examples.Games.Heart;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Examples.Games.Tests.Heart
{
    public class HeartTests
    {
        [Fact]
        public void CorrectInitialAvailableMoves()
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            var state = HeartState.Initial(p1Hand, p2Hand);
            state.GetAvailableMoves().ToArray().ShouldBe(new GridMove[] { 0, 1, 2, 3 });
        }

        [Fact]
        public void CanDraw()
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            IGameState<GridMove, WinLoseDrawResult?> state = HeartState.Initial(p1Hand, p2Hand);
            state = state.Move(new GridMove(0)); // attack
            state = state.Move(new GridMove(0)); // attack

            state = state.Move(new GridMove(1)); // feint
            state = state.Move(new GridMove(1)); // feint

            state = state.Move(new GridMove(2)); // defend
            state = state.Move(new GridMove(2)); // defend

            state = state.Move(new GridMove(3)); // dodge
            state = state.Move(new GridMove(3)); // dodge

            state.GetResult().ShouldBe(WinLoseDrawResult.Draw);
        }

        [Fact]
        public void CanWinAsP1()
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            IGameState<GridMove, WinLoseDrawResult?> state = HeartState.Initial(p1Hand, p2Hand);
            state = state.Move(new GridMove(1)); // feint
            state = state.Move(new GridMove(2)); // defend

            state = state.Move(new GridMove(3)); // dodge
            state = state.Move(new GridMove(0)); // attack

            state = state.Move(new GridMove(0)); // attack
            state = state.Move(new GridMove(1)); // feint

            state = state.Move(new GridMove(2)); // defend
            state = state.Move(new GridMove(3)); // dodge

            state.GetResult().ShouldBe(WinLoseDrawResult.Win);
        }
    }
}
