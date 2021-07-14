using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Examples.Chess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Chess
{
    public class ChessState : IGameState<ChessMove, ChessResult>
    {
        private readonly IGame game;

        private ChessState(IGame game) 
        {
            this.game = game;
        }

        public static ChessState Initial()
        {
            //var board = new Board();
            //var pieceValue = new PieceValue();
            //var position = new Position(board, pieceValue);
            //var game = GameFactory.Create(position);
            //game.NewGame(Fen.StartPositionFen);

            return new ChessState(null);
        }

        public PlayerIndex CurrentPlayerIndex => throw new NotImplementedException();

        public IGameState<ChessMove, ChessResult> Move(ChessMove move)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChessMove> GetAvailableMoves()
        {
            throw new NotImplementedException();
        }

        public ChessResult GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
