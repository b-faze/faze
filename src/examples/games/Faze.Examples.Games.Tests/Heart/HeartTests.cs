using Faze.Abstractions.GameMoves;
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
        public void CanPlayRound()
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            IGameState<GridMove, HeartResult?> state = HeartState.Initial(p1Hand, p2Hand);
            state = state.Move(new GridMove(0));
            state = state.Move(new GridMove(0));

            state = state.Move(new GridMove(1));
            state = state.Move(new GridMove(1));

            state = state.Move(new GridMove(2));
            state = state.Move(new GridMove(2));

            state = state.Move(new GridMove(3));
            state = state.Move(new GridMove(3));

            state.GetResult().ShouldBe(new HeartResult { P1Points = 1, P2Points = 1 });
        }

        [Fact]
        public void CanCompoundAdvantage()
        {
            var p1Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };
            var p2Hand = new HeartMove[] { new AttackMove(), new FeintMove(), new DefendMove(), new DodgeMove() };

            IGameState<GridMove, HeartResult?> state = HeartState.Initial(p1Hand, p2Hand);
            state = state.Move(new GridMove(1)); // feint
            state = state.Move(new GridMove(2)); // defend

            state = state.Move(new GridMove(3)); // dodge
            state = state.Move(new GridMove(0)); // attack

            state = state.Move(new GridMove(0)); // attack
            state = state.Move(new GridMove(1)); // feint

            state = state.Move(new GridMove(2)); // defend
            state = state.Move(new GridMove(3)); // dodge

            var result = state.GetResult();
            result?.P1Points.ShouldBe(3);
            result?.P2Points.ShouldBe(0);
        }
    }
}
