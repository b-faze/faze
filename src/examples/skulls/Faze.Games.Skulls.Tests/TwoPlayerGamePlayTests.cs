using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Games.Skulls;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace Faze.Games.Skulls.Tests
{
    public class TwoPlayerGamePlayTests
    {
        private readonly PlayerIndex p1;
        private readonly PlayerIndex p2;
        private readonly SkullsState initialState;

        public TwoPlayerGamePlayTests()
        {
            p1 = 1;
            p2 = 2;
            var players = new PlayerIndex[] { p1, p2 };
            initialState = SkullsState.Initial(players);
        }

        [Fact]
        public void GameStartsWithOnlyPlacementMoves()
        {
            initialState.CurrentPlayerIndex.ShouldBe(p1);

            var availableMoves = initialState.GetAvailableMoves();
            availableMoves.ShouldAllBe(x => x is SkullsPlacementMove);
        }

        [Fact]
        public void GameExample1()
        {
            IGameState<ISkullsMove, SkullsResult> state = initialState;

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayerIndex.ShouldBe(p2);

            state.GetAvailableMoves().ShouldAllBe(x => x is SkullsPlacementMove);

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayerIndex.ShouldBe(p1);

            state.GetAvailableMoves().Count(x => x is SkullsBetMove).ShouldBe(2, "2 possible bet moves available");

            // max bet from p1
            state = state.Move(new SkullsBetMove(2));

            state.CurrentPlayerIndex.ShouldBe(p1, "p1 is still the current player as they need to start revealing");

            var availableMoves = state.GetAvailableMoves().ToArray();
            availableMoves.Length.ShouldBe(1, "p1 needs to reveal their top token first");
            var revealMove = availableMoves.First().ShouldBeOfType<SkullsRevealMove>();
            revealMove.TargetPlayer.ShouldBe(p1, "p1 to reveal their own stack first");

            state = state.Move(revealMove);

            state.CurrentPlayerIndex.ShouldBe(p1, "p1 still needs to reveal 1 more for bet of 2");

            availableMoves = state.GetAvailableMoves().ToArray();
            availableMoves.Length.ShouldBe(1);
            availableMoves.First().ShouldBeOfType<SkullsRevealMove>()
                .TargetPlayer.ShouldBe(p2, "p1 to reveal p2's stack as it is the only option");

            state = state.Move(new SkullsRevealMove(p2));

            state.CurrentPlayerIndex.ShouldBe(p2, "p1 has won their first bet, player 'left' of p1 to start next round");

            state.GetAvailableMoves().ShouldAllBe(x => x is SkullsPlacementMove);

            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));
            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));
            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Flower));

            state.CurrentPlayerIndex.ShouldBe(p1);

            state = state.Move(new SkullsBetMove(1));
            state = state.Move(SkullsBetMove.Skip());

            state = state.Move(new SkullsRevealMove(p1));

            state.GetResult().IsWinningPlayer(p1).ShouldBeTrue();
        }

        [Fact]
        public void WinByElimination()
        {
            IGameState<ISkullsMove, SkullsResult> state = initialState;

            state = WinByEliminationHelper(state);
            state = WinByEliminationHelper(state);
            state = WinByEliminationHelper(state);
            state = WinByEliminationHelper(state);

            state.GetResult().ShouldNotBeNull();
            state.GetResult().IsWinningPlayer(p1).ShouldBeTrue();
        }

        private IGameState<ISkullsMove, SkullsResult> WinByEliminationHelper(IGameState<ISkullsMove, SkullsResult> state)
        {            
            // initial placement
            state = state.Move(new SkullsPlacementMove(SkullsTokenType.Skull));
            state = state.Move(state.GetAvailableMoves().First());

            state = state.Move(new SkullsBetMove(1));
            state = state.Move(new SkullsBetMove(2));

            state.CurrentPlayerIndex.ShouldBe(p2, "p2 has made the max bet");

            state = state.Move(new SkullsRevealMove(p2));
            state = state.Move(new SkullsRevealMove(p1));

            // p2 penalty for picking a skull
            state = state.Move(new SkullsPenaltyDiscardMove(0));

            return state;
        }
    }
}
