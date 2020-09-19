using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using Faze.Instances.Games.Skulls;
using Shouldly;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace Faze.Games.Skulls.Tests
{
    public class TwoPlayerGamePlayTests
    {
        private readonly int p1;
        private readonly int p2;
        private readonly SkullsState<int> initialState;

        public TwoPlayerGamePlayTests()
        {
            p1 = 1;
            p2 = 2;
            var players = new int[] { p1, p2 };
            initialState = SkullsState<int>.Initial(players);
        }

        [Fact]
        public void GameStartsWithOnlyPlacementMoves()
        {
            initialState.CurrentPlayer.ShouldBe(p1);

            var availableMoves = initialState.AvailableMoves;
            availableMoves.ShouldAllBe(x => x is SkullsPlacementMove);
        }

        [Fact]
        public void GameExample1()
        {
            IGameState<ISkullsMove, WinLoseResult<int>, int> state = initialState;

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayer.ShouldBe(p2);

            state.AvailableMoves.ShouldAllBe(x => x is SkullsPlacementMove);

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayer.ShouldBe(p1);

            state.AvailableMoves.Count(x => x is SkullsBetMove).ShouldBe(2, "2 possible bet moves available");

            // max bet from p1
            state = state.Move(new SkullsBetMove(2));

            state.CurrentPlayer.ShouldBe(p1, "p1 is still the current player as they need to start revealing");

            state.AvailableMoves.Length.ShouldBe(1, "p1 needs to reveal their top token first");
            var revealMove = state.AvailableMoves.First().ShouldBeOfType<SkullsRevealMove>();
            revealMove.PlayerIndex.ShouldBe(0, "p1 to reveal their own stack first");

            state = state.Move(revealMove);

            state.CurrentPlayer.ShouldBe(p1, "p1 still needs to reveal 1 more for bet of 2");

            state.AvailableMoves.Length.ShouldBe(1);
            state.AvailableMoves.First().ShouldBeOfType<SkullsRevealMove>()
                .PlayerIndex.ShouldBe(1, "p1 to reveal p2's stack as it is the only option");

            state = state.Move(new SkullsRevealMove(1));

            state.CurrentPlayer.ShouldBe(p2, "p1 has won their first bet, player 'left' of p1 to start next round");

            state.AvailableMoves.ShouldAllBe(x => x is SkullsPlacementMove);

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));
            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));
            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayer.ShouldBe(p1);

            state = state.Move(new SkullsBetMove(1));
            state = state.Move(SkullsBetMove.Skip());

            state = state.Move(new SkullsRevealMove(0));

            state.Result.ResultFor(p1).ShouldBe(WinLoseDrawResult.Win);

        }
    }
}
