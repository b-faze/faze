using Faze.Abstractions.GameStates;
using Faze.Games.Chess.Rendering;
using Faze.Rendering.TreeLinq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Faze.Games.Chess.Tests.RenderingTests
{
    public class AvailableChessSquaresRendererTests
    {
        [Fact]
        public void RenderTest()
        {
            var renderer = new AvailableChessSquaresRenderer();

            IGameState<ChessMove, ChessResult<int>, int> state = ChessState<int>.Initial(1, 2);

            renderer.Draw(state, 1, "../../../something.png");
        }
    }
}
