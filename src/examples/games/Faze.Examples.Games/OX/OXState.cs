using System;
using System.Collections.Generic;
using System.Linq;
using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;

namespace Faze.Examples.Games.OX
{
    public struct OXState : IGameState<GridMove, WinLoseDrawResult?>
    {
        private const int MAX_MOVES = 9;
        private static int[] WinningStates = new[] { 292, 146, 73, 448, 56, 7, 273, 84 };

        private HashSet<GridMove> availableMoves;
        private bool p1Turn;
        private int p1Moves;
        private int p2Moves;

        public static OXState Initial => new OXState(0, 0, true, new HashSet<GridMove> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

        private OXState(int p1Moves, int p2Moves, bool p1Turn, HashSet<GridMove> availableMoves)
        {
            this.p1Turn = p1Turn;

            this.availableMoves = new HashSet<GridMove>();
            this.availableMoves.UnionWith(availableMoves);

            this.p1Moves = p1Moves;
            this.p2Moves = p2Moves;
        }

        public int Dimension => 3;
        public int TotalPlayers => 2;
        public PlayerIndex CurrentPlayerIndex => p1Turn ? PlayerIndex.P1 : PlayerIndex.P2;

        public IEnumerable<GridMove> GetAvailableMoves() => GetResult() != null ? new GridMove[0].AsEnumerable() : availableMoves;

        public IGameState<GridMove, WinLoseDrawResult?> Move(GridMove move)
        {
            var newP1Moves = p1Moves;
            var newP2Moves = p2Moves;
            var newAvailableMoves = new HashSet<GridMove>(availableMoves);

            var bitwiseMove = 1 << move;
            if (p1Turn)
            {
                newP1Moves |= bitwiseMove;
            }
            else
            {
                newP2Moves |= bitwiseMove;
            }

            newAvailableMoves.Remove(move);

            return new OXState(newP1Moves, newP2Moves, !p1Turn, newAvailableMoves);
        }

        public WinLoseDrawResult? GetResult()
        {
            // minimum of 5 moves is required to end the game
            if (MAX_MOVES - availableMoves.Count < 5) return null;

            foreach (var state in WinningStates)
            {
                if ((p1Moves & state) == state) return WinLoseDrawResult.Win;
                if ((p2Moves & state) == state) return WinLoseDrawResult.Lose;
            }

            return availableMoves.Count == 0 ? WinLoseDrawResult.Draw : (WinLoseDrawResult?)null;
        }
    }

}
