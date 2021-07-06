using Faze.Abstractions.GameStates;
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
    public class ChessState<TPlayer> : IGameState<ChessMove, ChessResult<TPlayer>, TPlayer>
    {
        private readonly TPlayer p1;
        private readonly TPlayer p2;

        private readonly IGame game;

        private ChessState(TPlayer p1, TPlayer p2, IGame game) 
        {
            this.p1 = p1;
            this.p2 = p2;
            this.game = game;
        }

        public static ChessState<TPlayer> Initial(TPlayer p1, TPlayer p2)
        {
            var board = new Board();
            var pieceValue = new PieceValue();
            var position = new Position(board, pieceValue);
            var game = GameFactory.Create(position);
            game.NewGame(Fen.StartPositionFen);

            return new ChessState<TPlayer>(p1, p2, game);
        }

        public TPlayer CurrentPlayer => game.CurrentPlayer().IsWhite ? p1 : p2;
        public ChessMove[] AvailableMoves => GetAvailableMoves().ToArray();
        public ChessResult<TPlayer> Result => GetResult();

        public IGameState<ChessMove, ChessResult<TPlayer>, TPlayer> Move(ChessMove move)
        {
            var board = new Board();
            var pieceValue = new PieceValue();
            var position = new Position(board, pieceValue);
            var newGame = GameFactory.Create(position);
            newGame.NewGame(game.GetFen().ToString());

            newGame.Pos.MakeMove(move.move, new State());

            return new ChessState<TPlayer>(p1, p2, newGame);
        }

        private IEnumerable<ChessMove> GetAvailableMoves()
        {
            return game.Pos.GenerateMoves().Select(move => new ChessMove(move));
        }

        private ChessResult<TPlayer> GetResult()
        {
            var pos = game.Pos;

            if (pos.IsMate) {
                var winningPlayer = CurrentPlayer.Equals(p1) ? p2 : p1;
                return ChessResult<TPlayer>.CheckMate(winningPlayer);
            }

            return null;
        }
    }
}
