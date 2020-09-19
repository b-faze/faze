using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    public class SkullsPlayerEnvironments
    {
        private readonly SkullsPlayerEnvironment[] environments;
        private IEnumerable<SkullsPlayerEnvironment> activeEnvironments => environments.Where(x => !x.IsOut);

        private SkullsPlayerEnvironments(SkullsPlayerEnvironment[] environments)
        {
            this.environments = environments;
        }

        public static SkullsPlayerEnvironments Initial(int players)
        {
            var playerEnvironments = new SkullsPlayerEnvironment[players];
            for (var i = 0; i < players; i++)
            {
                playerEnvironments[i] = SkullsPlayerEnvironment.Initial();
            }

            return new SkullsPlayerEnvironments(playerEnvironments);
        }

        internal int GetMaxPossibleBet()
        {
            return environments.Sum(x => x.Stack.Length);
        }

        internal SkullsPlayerEnvironments Discard(int playerIndexWithPenalty, int handIndex)
        {
            var clone = Clone();
            clone.environments[playerIndexWithPenalty].Discard(handIndex);

            return clone;
        }

        internal bool AnyPlayersStillToBet()
        {
            return environments.Any(x => x.Bet == null);
        }

        internal IEnumerable<int> GetBettingPlayerIndexes()
        {
            for (var i = 0; i < environments.Length; i++)
            {
                if (!environments[i].Bet.Skipped)
                    yield return i;
            }
        }

        internal SkullsPlayerEnvironments Bet(int playerIndex, SkullsBetMove betMove)
        {
            var clone = Clone();
            clone.environments[playerIndex].SetBet(betMove);

            return clone;
        }

        public SkullsPlayerEnvironments Place(int playerIndex, SkullsTokenType token)
        {
            var clone = Clone();
            clone.environments[playerIndex].Place(token);

            return clone;
        }

        internal IEnumerable<SkullsRevealMove> GetRevealMoves()
        {
            for (var i = 0; i < environments.Length; i++)
            {
                if (environments[i].CanReveal())
                    yield return new SkullsRevealMove(i);
            }
        }

        internal int? GetCurrentMaxBet()
        {
            return environments
                .Select(x => x.Bet)
                .Where(x => x != null)
                .Max(x => x.Bet);
        }

        public SkullsPlayerEnvironments Reveal(int playerIndex)
        {
            var clone = Clone();
            clone.environments[playerIndex].Reveal();

            return clone;
        }

        internal bool IsSkullRevealed()
        {
            return activeEnvironments.Any(x => x.IsSkullRevealed());
        }

        internal int GetNextPlayerIndex(int currentPlayerIndex)
        {
            for (var i = 1; i < environments.Length; i++)
            {
                var nextIndex = (currentPlayerIndex + i) % environments.Length;
                var nextEnvironment = environments[nextIndex];
                if (nextEnvironment.IsOut)
                    continue;

                return nextIndex;
            }

            return currentPlayerIndex;
        }

        internal SkullsPlayerEnvironment GetForPlayer(int playerIndex)
        {
            return environments[playerIndex];
        }

        internal SkullsPlayerEnvironments Clone()
        {
            var newEnvironments = environments.Select(x => x.Clone()).ToArray();
            return new SkullsPlayerEnvironments(newEnvironments);
        }

        internal int GetTotalRevealed()
        {
            return activeEnvironments.Sum(x => x.GetTotalRevealed());
        }

        internal SkullsPlayerEnvironments MarkWinning(int currentPlayerIndex)
        {
            var clone = Clone();
            clone.environments[currentPlayerIndex].MarkWinning();
            foreach (var environment in clone.environments)
            {
                environment.PickUpStacks();
            }

            return clone;
        }
    }
}
