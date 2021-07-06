using Faze.Abstractions.Players;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Xunit;

namespace Faze.Games.Chess.Tests
{
    public class FoolsCheckMateTests
    {
        [Fact]
        public void FoolsCheckMateTest()
        {
            var moves = new List<ChessMove>(4) 
            {
                    ChessMove.Create(ChessSquares.f2, ChessSquares.f3),
                    ChessMove.Create(ChessSquares.e7, ChessSquares.e5),
                    ChessMove.Create(ChessSquares.g2, ChessSquares.g4),
                    ChessMove.Create(ChessSquares.d8, ChessSquares.h4)
            };

            var p1 = PlayerIndex.P1;
            var p2 = PlayerIndex.P2;
            var state = ChessState.Initial();

            foreach (var move in moves)
            {
                state.Move(move);
            }

            state.GetResult().ShouldNotBeNull();
            state.GetResult().IsCheckMate.ShouldBeTrue();
            state.GetResult().WinningPlayerIndex.ShouldBe(p2);

            state.GetAvailableMoves().ShouldBeEmpty();
        }
    }
}
