using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    public class SkullsPlayerEnvironment
    {
        public static  SkullsPlayerEnvironment Initial()
        {
            return new SkullsPlayerEnvironment
            {
                Stack = new SkullsTokenType[0],
                Hand = new SkullsTokenType[]
                {
                    SkullsTokenType.Flower,
                    SkullsTokenType.Flower,
                    SkullsTokenType.Flower,
                    SkullsTokenType.Skull
                },
                RevealedStack = new SkullsTokenType[0]
            };
        }

        public SkullsTokenType[] Stack { get; private set; }
        public SkullsTokenType[] RevealedStack { get; private set; }
        public SkullsTokenType[] Hand { get; private set; }
        public bool HasWin { get; private set; }
        public bool IsOut { get; private set; }
        public SkullsBetMove? Bet { get; private set; }

        public void Place(SkullsTokenType token)
        {
            var tokenIndexInHand = Hand.TakeWhile(t => t != token).Count();
            if (tokenIndexInHand == Hand.Length)
                throw new Exception($"Cannot place a token which isn't in the player's hand. No '{token}' in [{string.Join(", ", Hand)}]");

            Stack = Stack.Concat(new[] { token }).ToArray();
            Hand = Hand.Take(tokenIndexInHand).Concat(Hand.Skip(tokenIndexInHand + 1)).ToArray();
        }

        internal void SetBet(SkullsBetMove betMove)
        {
            Bet = betMove;
        }

        internal void Discard(int handIndex)
        {
            Hand = Hand.Take(handIndex).Concat(Hand.Skip(handIndex + 1)).ToArray();
        }

        public void Reveal()
        {
            var revealedToken = Stack[Stack.Length - 1];
            Stack = Stack.Take(Stack.Length - 1).ToArray();
            RevealedStack = RevealedStack.Concat(new[] { revealedToken }).ToArray();
        }

        internal bool IsSkullRevealed()
        {
            return RevealedStack.Any(x => x == SkullsTokenType.Skull);
        }

        internal IEnumerable<ISkullsMove> GetPlacementMoves()
        {
            return Hand.Select(x => (ISkullsMove)new SkullsPlacementMove(x));
        }

        internal IEnumerable<ISkullsMove> GetPenaltyDiscardMoves()
        {
            return Hand.Select((x, i) => (ISkullsMove)new SkullsPenaltyDiscardMove(i));
        }

        internal int GetTotalRevealed()
        {
            return RevealedStack.Length;
        }

        internal void MarkWinning()
        {
            HasWin = true;
        }

        internal bool CanReveal()
        {
            return Stack.Length > 0;
        }

        internal SkullsPlayerEnvironment Clone()
        {
            return new SkullsPlayerEnvironment
            {
                Stack = Stack.ToArray(),
                Hand = Hand.ToArray(),
                RevealedStack = RevealedStack.ToArray(),
                HasWin = HasWin,
                Bet = Bet
            };
        }

        internal void PickUpStacks()
        {
            Hand = Hand.Concat(Stack).Concat(RevealedStack).ToArray();
            Stack = new SkullsTokenType[0];
            RevealedStack = new SkullsTokenType[0];
        }
    }
}
