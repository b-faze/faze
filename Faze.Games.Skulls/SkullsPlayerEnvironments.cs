using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    public class SkullsPlayerEnvironments<TPlayer>
    {
        private readonly SkullsPlayerEnvironment<TPlayer>[] environments;
        private IEnumerable<SkullsPlayerEnvironment<TPlayer>> activeEnvironments => environments.Where(x => !x.IsOut);

        private SkullsPlayerEnvironments(SkullsPlayerEnvironment<TPlayer>[] environments)
        {
            this.environments = environments;
        }

        public static SkullsPlayerEnvironments<TPlayer> Initial(TPlayer[] players)
        {
            var playerEnvironments = new SkullsPlayerEnvironment<TPlayer>[players.Length];
            for (var i = 0; i < players.Length; i++)
            {
                playerEnvironments[i] = SkullsPlayerEnvironment<TPlayer>.Initial(players[i]);
            }

            return new SkullsPlayerEnvironments<TPlayer>(playerEnvironments);
        }

        internal int GetMaxPossibleBet()
        {
            return environments.Sum(x => x.Stack.Length);
        }

        internal int GetPlayerIndex(TPlayer player)
        {
            for (var i = 0; i < environments.Length; i++)
            {
                if (environments[i].Player.Equals(player))
                    return i;
            }

            throw new Exception("player does not exist");
        }

        internal SkullsPlayerEnvironments<TPlayer> Discard(int playerIndexWithPenalty, int handIndex)
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
                if (!environments[i].Bet.Value.Skipped)
                    yield return i;
            }
        }

        internal SkullsPlayerEnvironments<TPlayer> Bet(int playerIndex, SkullsBetMove betMove)
        {
            var clone = Clone();
            clone.environments[playerIndex].SetBet(betMove);

            return clone;
        }

        public SkullsPlayerEnvironments<TPlayer> Place(int playerIndex, SkullsTokenType token)
        {
            var clone = Clone();
            clone.environments[playerIndex].Place(token);

            return clone;
        }

        internal IEnumerable<ISkullsMove> GetRevealMoves()
        {
            for (var i = 0; i < environments.Length; i++)
            {
                if (environments[i].CanReveal())
                    yield return new SkullsRevealMove<TPlayer>(environments[i].Player);
            }
        }

        internal int? GetCurrentMaxBet()
        {
            return environments
                .Select(x => x.Bet)
                .Where(x => x != null)
                .Max(x => x.Value.Bet);
        }

        public SkullsPlayerEnvironments<TPlayer> Reveal(TPlayer targetPlayer)
        {
            var clone = Clone();
            clone.environments.First(x => x.Player.Equals(targetPlayer)).Reveal();

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

        internal SkullsPlayerEnvironment<TPlayer> GetForPlayer(int playerIndex)
        {
            return environments[playerIndex];
        }

        internal SkullsPlayerEnvironments<TPlayer> Clone()
        {
            var newEnvironments = environments.Select(x => x.Clone()).ToArray();
            return new SkullsPlayerEnvironments<TPlayer>(newEnvironments);
        }

        internal int GetTotalRevealed()
        {
            return activeEnvironments.Sum(x => x.GetTotalRevealed());
        }

        internal SkullsPlayerEnvironments<TPlayer> MarkWinning(int currentPlayerIndex)
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
