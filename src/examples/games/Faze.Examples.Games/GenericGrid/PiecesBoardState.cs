using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Faze.Examples.Games.GridGames
{
    public class PiecesBoardState : IGameState<GridMove, SingleScoreResult?>
    {
        private PiecesBoardStateConfig config;
        private List<GridMove> currentInfluence;
        private List<GridMove> availableMoves;
        private int currentScore;
        private bool fail;

        public PiecesBoardState(PiecesBoardStateConfig config)
            : this(config, new GridMove[0], Enumerable.Range(0, config.Dimension * config.Dimension).Select(x => (GridMove)x), 0, false)
        {
        }

        protected PiecesBoardState(PiecesBoardStateConfig config, IEnumerable<GridMove> influence, IEnumerable<GridMove> availableMoves, int currentScore, bool fail)
        {
            this.config = config;
            this.currentInfluence = influence.ToList();
            this.availableMoves = availableMoves.ToList();
            this.currentScore = currentScore;
            this.fail = fail;
        }

        public int TotalPlayers => 1;
        public PlayerIndex CurrentPlayerIndex => 0;

        public IEnumerable<GridMove> GetAvailableMoves() => fail ? new GridMove[0].AsEnumerable() : availableMoves;

        public IGameState<GridMove, SingleScoreResult?> Move(GridMove move)
        {
            if (!availableMoves.Contains(move))
                throw new InvalidDataException($"Move {move} has already been made");

            if (currentInfluence.Contains(move))
                return new PiecesBoardState(config, currentInfluence, availableMoves, currentScore, fail: true);

            var moveInfluence = config.Piece.GetPieceMoves(move, config.Dimension);
            var newAvailableMoves = availableMoves.Except(new[] { move });
            var newInfluence = currentInfluence.Union(moveInfluence);

            var availableGoodMoves = newAvailableMoves.Except(newInfluence);
            if (config.OnlySafeMoves || !availableGoodMoves.Any())
                newAvailableMoves = availableGoodMoves;

            return new PiecesBoardState(config, newInfluence, newAvailableMoves, currentScore + 1, fail: false);
        }

        public SingleScoreResult? GetResult()
        {
            if (fail)
                return new SingleScoreResult(-1);

            return !availableMoves.Any()
                ? new SingleScoreResult(currentScore)
                : (SingleScoreResult?)null;
        }
    }

}
