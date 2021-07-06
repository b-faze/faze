using System;
using System.Collections.Generic;
using System.Linq;
using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;

namespace Faze.Examples.OX
{
    public struct OXState<IAgent> : IGameState<int, WinLoseDrawResult?, IAgent>
    {
        private const int MAX_MOVES = 9;
        private static int[] WinningStates = new[] { 292, 146, 73, 448, 56, 7, 273, 84 };

        private IAgent p1;
        private IAgent p2;
        private HashSet<int> availableMoves;
        private bool p1Turn;
        private int p1Moves;
        private int p2Moves;

        public static OXState<IAgent> Initial(IAgent p1, IAgent p2) => new OXState<IAgent>(p1, p2, 0, 0, true, new HashSet<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

        private OXState(IAgent p1, IAgent p2, int p1Moves, int p2Moves, bool p1Turn, HashSet<int> availableMoves)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p1Turn = p1Turn;
            this.availableMoves = new HashSet<int>();
            this.availableMoves.UnionWith(availableMoves);
            this.p1Moves = p1Moves;
            this.p2Moves = p2Moves;
        }

        public int Dimension => 3;
        public int TotalPlayers => 2;
        public IAgent GetCurrentPlayer() => p1Turn ? p1 : p2;

        public IEnumerable<int> GetAvailableMoves() => availableMoves;

        public WinLoseDrawResult? Result => GetResult();

        public IGameState<int, WinLoseDrawResult?, IAgent> Move(int move)
        {
            var newP1Moves = p1Moves;
            var newP2Moves = p2Moves;
            var newAvailableMoves = new HashSet<int>(availableMoves);

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

            return new OXState<IAgent>(p1, p2, newP1Moves, newP2Moves, !p1Turn, newAvailableMoves);
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
