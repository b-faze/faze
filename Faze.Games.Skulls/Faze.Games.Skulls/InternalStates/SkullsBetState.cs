using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Instances.Games.Skulls
{
    public class SkullsBetState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsBetState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
            : base(players, playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(SkullsMove move)
        {
            if (!(move is SkullsBetMove betMove))
                throw new Exception("Not supported move");

            var newPlayerEnvironments = playerEnvironments.Bet(currentPlayerIndex, betMove);

            var bet = betMove.Bet;
            var maxBet = newPlayerEnvironments.GetMaxPossibleBet();
            if (bet == maxBet)
                return new SkullsRevealState<TPlayer>(players, newPlayerEnvironments, currentPlayerIndex, bet.Value);

            // if everyone else has skipped, move on to the reveal
            if (!newPlayerEnvironments.AnyPlayersStillToBet())
            {
                var playerIndexesWithBets = newPlayerEnvironments.GetBettingPlayerIndexes().ToArray();
                if (playerIndexesWithBets.Length == 1)
                {
                    var onlyBetPlayerIndex = playerIndexesWithBets[0];
                    return new SkullsRevealState<TPlayer>(players, newPlayerEnvironments, onlyBetPlayerIndex, playerEnvironments.GetForPlayer(onlyBetPlayerIndex).Bet.Bet.Value);
                }
            }

            var newPlayerIndex = newPlayerEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            return new SkullsBetState<TPlayer>(players, newPlayerEnvironments, newPlayerIndex);
        }

        protected override SkullsMove[] GetAvailableMoves()
        {
            var maxBet = playerEnvironments.GetMaxPossibleBet();
            var currentMaxBet = playerEnvironments.GetCurrentMaxBet();

            var avaliableBets = new List<SkullsBetMove>();
            avaliableBets.Add(SkullsBetMove.Skip());
            avaliableBets.AddRange(Enumerable.Range(1, maxBet)
                .Where(x => x > currentMaxBet)
                .Select(x => new SkullsBetMove(x)));

            return avaliableBets.ToArray();
        }
    }
}
