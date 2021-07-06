using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Rudz.Chess;
using Rudz.Chess.Factories;
using Rudz.Chess.Fen;
using Rudz.Chess.MoveGeneration;
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
            var board = new Board();
            var pieceValue = new PieceValue();
            var position = new Position(board, pieceValue);
            var game = GameFactory.Create(position);
            game.NewGame(Fen.StartPositionFen);

            return new ChessState(game);
        }

        public PlayerIndex CurrentPlayerIndex => game.CurrentPlayer().IsWhite ? PlayerIndex.P1 : PlayerIndex.P2;

        public IGameState<ChessMove, ChessResult> Move(ChessMove move)
        {
            var board = new Board();
            var pieceValue = new PieceValue();
            var position = new Position(board, pieceValue);
            var newGame = GameFactory.Create(position);
            newGame.NewGame(game.GetFen().ToString());

            newGame.Pos.MakeMove(move.move, new State());

            return new ChessState(newGame);
        }

        public IEnumerable<ChessMove> GetAvailableMoves()
        {
            return game.Pos.GenerateMoves().Select(move => new ChessMove(move));
        }

        public ChessResult GetResult()
        {
            var pos = game.Pos;

            if (pos.IsMate) {
                var winningPlayer = CurrentPlayerIndex.Equals(PlayerIndex.P1) ? PlayerIndex.P2 : PlayerIndex.P1;
                return ChessResult.CheckMate(winningPlayer);
            }

            return null;
        }
    }
}
