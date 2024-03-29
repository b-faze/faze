﻿using Faze.Abstractions.GameStates;
using Faze.Games.Chess.Rendering;
using Faze.Core.TreeLinq;
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

            IGameState<ChessMove, ChessResult> state = ChessState.Initial();

            renderer.Draw(state, 1, "../../../something.png");
        }
    }
}
