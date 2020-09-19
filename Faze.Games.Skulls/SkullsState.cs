using Faze.Abstractions;
using Faze.Abstractions.GameResults;

namespace Faze.Instances.Games.Skulls
{
    public abstract class SkullsState<TPlayer> : IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer>
    {
        protected readonly TPlayer[] players;
        protected SkullsPlayerEnvironments playerEnvironments;
        protected int currentPlayerIndex = 0;

        protected SkullsState(TPlayer[] players, SkullsPlayerEnvironments playerEnvironments, int currentPlayerIndex)
        {
            this.players = players;
            this.playerEnvironments = playerEnvironments;
            this.currentPlayerIndex = currentPlayerIndex;
        }

        public static SkullsState<TPlayer> Initial(TPlayer[] players)
        {
            var playerEnvironments = SkullsPlayerEnvironments.Initial(players.Length);
            return new SkullsInitialPlacementState<TPlayer>(players, playerEnvironments, 0);
        }

        public TPlayer CurrentPlayer => players[currentPlayerIndex];
        public SkullsMove[] AvailableMoves => GetAvailableMoves();
        public WinLoseResult<TPlayer> Result { get; protected set; }

        public abstract IGameState<SkullsMove, WinLoseResult<TPlayer>, TPlayer> Move(SkullsMove move);

        protected abstract SkullsMove[] GetAvailableMoves();
    }
}
