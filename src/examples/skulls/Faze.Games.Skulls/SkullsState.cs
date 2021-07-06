using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Games.Skulls;
using System.Collections.Generic;

namespace Faze.Games.Skulls
{
    public abstract class SkullsState : IGameState<ISkullsMove, SkullsResult>
    {
        protected  readonly SkullsPlayerEnvironments playerEnvironments;
        protected readonly int currentPlayerIndex;

        protected SkullsState(SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
        {
            this.playerEnvironments = playerEnvironments;
            this.currentPlayerIndex = currentPlayerIndex;
        }

        public static SkullsState Initial(PlayerIndex[] players)
        {
            var playerEnvironments = SkullsPlayerEnvironments.Initial(players);
            return new SkullsInitialPlacementState(playerEnvironments, 0);
        }

        public PlayerIndex CurrentPlayerIndex => playerEnvironments.GetForPlayer(currentPlayerIndex).Player;
        public SkullsResult Result { get; protected set; }
        public SkullsResult GetResult() => Result;

        public abstract IGameState<ISkullsMove, SkullsResult> Move(ISkullsMove move);

        public abstract IEnumerable<ISkullsMove> GetAvailableMoves();
    }
}
