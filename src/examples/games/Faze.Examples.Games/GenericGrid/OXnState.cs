using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Examples.Games.GridGames
{
    public struct OXnState : IGameState<GridMove, WinLoseDrawResult?>
    {
        private int MaxMoves => Dimention * Dimention;
        private readonly ulong[] WinningStates;

        private List<GridMove> availableMoves;
        private ulong p1Moves;
        private ulong p2Moves;
        private bool p1Turn;

        public OXnState(int n) : this(n, CalcWinningStates(n).ToArray())
        {
        }

        public OXnState(int n, ulong[] winningStates) : this(n, 0, 0, true, Enumerable.Range(0, n * n).Select(x => new GridMove(x)).ToList(), winningStates)
        {
        }

        private OXnState(int n, ulong p1Moves, ulong p2Moves, bool p1Turn, List<GridMove> availableMoves, ulong[] winningStates)
        {
            Dimention = n;
            WinningStates = winningStates;

            this.p1Turn = p1Turn;
            this.availableMoves = availableMoves.ToList();
            this.p1Moves = p1Moves;
            this.p2Moves = p2Moves;
        }

        public int Dimention { get; }
        public int TotalPlayers => 2;
        public PlayerIndex CurrentPlayerIndex => p1Turn ? PlayerIndex.P1 : PlayerIndex.P2;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            if (GetResult() != null)
                return new GridMove[0];

            return availableMoves;
        }

        public IGameState<GridMove, WinLoseDrawResult?> Move(GridMove move)
        {
            ulong bitwiseMove = 1uL << move;
            var newP1Moves = p1Moves;
            var newP2Moves = p2Moves;
            var newAvailableMoves = availableMoves.ToList();

            if (p1Turn)
            {
                newP1Moves |= bitwiseMove;
            }
            else
            {
                newP2Moves |= bitwiseMove;
            }

            newAvailableMoves.Remove(move);

            return new OXnState(Dimention, newP1Moves, newP2Moves, !p1Turn, newAvailableMoves, WinningStates);
        }

        public WinLoseDrawResult? GetResult()
        {
            // minimum of (Dimention * 2 - 1) moves is required to end the game
            if (MaxMoves - availableMoves.Count < Dimention * 2 - 1) return null;

            foreach (var state in WinningStates)
            {
                if ((p1Moves & state) == state) return WinLoseDrawResult.Win;
                if ((p2Moves & state) == state) return WinLoseDrawResult.Lose;
            }

            return availableMoves.Count == 0 ? WinLoseDrawResult.Draw : (WinLoseDrawResult?)null;
        }

        public static IEnumerable<ulong> CalcWinningStates(int dimension)
        {
            return CalcHorizontalStates(dimension)
                .Concat(CalcVerticalStates(dimension))
                .Concat(CalcPositiveDiagonalStates(dimension))
                .Concat(CalcNegativeDiagonalStates(dimension));
        }

        private static IEnumerable<ulong> CalcHorizontalStates(int dimension)
        {
            ulong mask = (ulong)Math.Pow(2, dimension) - 1;
            for (var j = 0; j < dimension; j++)
            {
                yield return mask << j * dimension;
            }
        }

        private static IEnumerable<ulong> CalcVerticalStates(int dimension)
        {
            ulong mask = 0;
            for (var i = 0; i < dimension; i++)
            {
                mask = mask << dimension;
                mask += 1;
            }

            for (var i = 0; i < dimension; i++)
            {
                yield return mask << i;
            }
        }

        private static IEnumerable<ulong> CalcPositiveDiagonalStates(int dimension)
        {
            ulong mask = 0;
            for (var i = 0; i < dimension; i++)
            {
                mask |= 1uL << i * (dimension + 1);
            }

            yield return mask;
        }

        private static IEnumerable<ulong> CalcNegativeDiagonalStates(int dimension)
        {
            ulong mask = 0;
            for (var i = 0; i < dimension; i++)
            {
                mask |= 1uL << i * (dimension - 1);
            }

            mask = mask << dimension - 1;

            yield return mask;
        }
    }
}
