using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.Heart
{
    public class HeartState : IGameState<GridMove, WinLoseDrawResult?>
    {
        const int maxPlayerMoves = 4;

        private HeartMove[] p1Hand;
        private HeartMove[] p2Hand;
        private HeartMove[] moves;

        public static HeartState Initial(HeartMove[] p1Hand, HeartMove[] p2Hand) => new HeartState(p1Hand, p2Hand, new HeartMove[0]);
        private HeartState(HeartMove[] p1Hand, HeartMove[] p2Hand, HeartMove[] moves)
        {
            this.p1Hand = p1Hand;
            this.p2Hand = p2Hand;
            this.moves = moves;
        }

        public PlayerIndex CurrentPlayerIndex => moves.Length % 2 == 0 ? PlayerIndex.P1 : PlayerIndex.P2;

        public IEnumerable<GridMove> GetAvailableMoves()
        {
            if (CurrentPlayerIndex == PlayerIndex.P1)
            {
                return p1Hand.Select(ToGridMove);
            }

            return p2Hand.Select(ToGridMove);
        }

        public WinLoseDrawResult? GetResult()
        {
            if (moves.Length < maxPlayerMoves * 2) 
                return null;

            var p1Points = 0;
            var p2Points = 0;
            var p1Advantage = 0;
            var p2Advantage = 0;
            for (var i = 0; i < moves.Length; i += 2)
            {
                var p1Move = moves[i];
                var p2Move = moves[i + 1];

                var p1Effect = p1Move.GetEffect(p2Move.Type);
                var p2Effect = p2Move.GetEffect(p1Move.Type);

                var hit = false;
                if (p1Effect.Hit > 0)
                {
                    p1Points += p1Effect.Hit + p1Advantage - p2Effect.Block;
                    hit = true;
                }

                if (p2Effect.Hit > 0)
                {
                    p2Points += p2Effect.Hit + p2Advantage - p1Effect.Block;
                    hit = true;
                }

                // if hit, reset advantages
                if (hit)
                {
                    p1Advantage = 0;
                    p2Advantage = 0;
                }

                var advantage = p1Effect.Advantage - p2Effect.Advantage;
                if (advantage > 0)
                {
                    p1Advantage += advantage;
                }
                else
                {
                    p2Advantage += advantage;
                }
            }

            if (p1Points == p2Points)
                return WinLoseDrawResult.Draw;

            return p1Points > p2Points ? WinLoseDrawResult.Win : WinLoseDrawResult.Lose;
        }

        public IGameState<GridMove, WinLoseDrawResult?> Move(GridMove gridMove)
        {
            var p1Turn = CurrentPlayerIndex == PlayerIndex.P1;
            var hand = p1Turn ? p1Hand : p2Hand;
            for (var i = 0; i < hand.Length; i++)
            {
                var move = hand[i];
                if (ToGridMove(move) == gridMove)
                {
                    var newHand = hand.Take(i).Concat(hand.Skip(i + 1));
                    var newP1Hand = p1Turn ? newHand : p1Hand;
                    var newP2Hand = p1Turn ? p2Hand : newHand;
                    var newMoves = moves.Concat(new HeartMove[] { move }).ToArray();
                    return new HeartState(newP1Hand.ToArray(), newP2Hand.ToArray(), newMoves);
                }
            }

            throw new Exception("Invalid move: " + gridMove);
        }

        private static GridMove ToGridMove(HeartMove move)
        {
            switch (move.Type)
            {
                case HeartMoveType.Attack:
                    return new GridMove(0);
                case HeartMoveType.Feint:
                    return new GridMove(1);
                case HeartMoveType.Defend:
                    return new GridMove(2);
                case HeartMoveType.Dodge:
                    return new GridMove(3);
            }

            throw new NotSupportedException($"Unknown move type '{move.Type}'");
        }
    }


}
