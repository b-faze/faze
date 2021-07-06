namespace Faze.Abstractions.GameStates
{
    public interface IGridGameState<TMove, out TResult, out TPlayer> : IGameState<TMove, TResult, TPlayer>
    {
        int GridSize { get; }
        new IGridGameState<TMove, TResult, TPlayer> Move(TMove move);
    }
}