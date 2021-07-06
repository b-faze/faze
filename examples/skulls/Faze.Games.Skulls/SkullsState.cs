using Faze.Abstractions;
using Faze.Abstractions.GameResults;
using Faze.Games.Skulls;

namespace Faze.Games.Skulls
{
    public abstract class SkullsState<TPlayer> : IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer>
    {
        protected  readonly SkullsPlayerEnvironments<TPlayer> playerEnvironments;
        protected readonly int currentPlayerIndex;

        protected SkullsState(SkullsPlayerEnvironments<TPlayer> playerEnvironments, int currentPlayerIndex)
        {
            this.playerEnvironments = playerEnvironments;
            this.currentPlayerIndex = currentPlayerIndex;
        }

        public static SkullsState<TPlayer> Initial(TPlayer[] players)
        {
            var playerEnvironments = SkullsPlayerEnvironments<TPlayer>.Initial(players);
            return new SkullsInitialPlacementState<TPlayer>(playerEnvironments, 0);
        }

        public TPlayer CurrentPlayer => playerEnvironments.GetForPlayer(currentPlayerIndex).Player;
        public ISkullsMove[] AvailableMoves => GetAvailableMoves();
        public SkullsResult<TPlayer> Result { get; protected set; }

        public abstract IGameState<ISkullsMove, SkullsResult<TPlayer>, TPlayer> Move(ISkullsMove move);

        protected abstract ISkullsMove[] GetAvailableMoves();
    }
}
