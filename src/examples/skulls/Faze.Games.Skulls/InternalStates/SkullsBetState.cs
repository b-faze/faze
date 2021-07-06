using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Games.Skulls
{
    public class SkullsBetState<TPlayer> : SkullsState<TPlayer>
    {
        public SkullsBetState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex)
            : base(playerEnvironments, currentPlayerIndex)
        {
        }

        public override IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move)
        {
            if (!(move is SkullsBetMove betMove))
                throw new Exception("Not supported move");

            var newPlayerEnvironments = playerEnvironments.Bet(currentPlayerIndex, betMove);

            var bet = betMove.Bet;
            var maxBet = newPlayerEnvironments.GetMaxPossibleBet();
            if (bet == maxBet)
                return new SkullsRevealState<TPlayer>(newPlayerEnvironments, currentPlayerIndex, bet.Value);

            // if everyone else has skipped, move on to the reveal
            if (!newPlayerEnvironments.AnyPlayersStillToBet())
            {
                var playerIndexesWithBets = newPlayerEnvironments.GetBettingPlayerIndexes().ToArray();
                if (playerIndexesWithBets.Length == 1)
                {
                    var onlyBetPlayerIndex = playerIndexesWithBets[0];
                    return new SkullsRevealState<TPlayer>(newPlayerEnvironments, onlyBetPlayerIndex, playerEnvironments.GetForPlayer(onlyBetPlayerIndex).Bet.Value.Bet.Value);
                }
            }

            var newPlayerIndex = newPlayerEnvironments.GetNextPlayerIndex(currentPlayerIndex);

            return new SkullsBetState<TPlayer>(newPlayerEnvironments, newPlayerIndex);
        }

        protected override ISkullsMove[] GetAvailableMoves()
        {
            var maxBet = playerEnvironments.GetMaxPossibleBet();
            var currentMaxBet = playerEnvironments.GetCurrentMaxBet();

            var avaliableBets = new List<ISkullsMove>();
            avaliableBets.Add(SkullsBetMove.Skip());
            avaliableBets.AddRange(Enumerable.Range(1, maxBet)
                .Where(x => x > currentMaxBet)
                .Select(x => (ISkullsMove)new SkullsBetMove(x)));

            return avaliableBets.ToArray();
        }
    }
}
