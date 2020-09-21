namespace Faze.Abstractions.GameStates
{
    public interface IGameState<TMove, out TResult, out TPlayer>
    {
        TPlayer CurrentPlayer { get; }

        TMove[] AvailableMoves { get; }

        TResult Result { get; }

        IGameState<TMove, TResult, TPlayer> Move(TMove move);
    }
}