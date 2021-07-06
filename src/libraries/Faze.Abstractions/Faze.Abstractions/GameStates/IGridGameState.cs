namespace Faze.Abstractions.GameStates
{
    public interface IGridGameState<TMove, out TResult> : IGameState<TMove, TResult>
    {
        int GridSize { get; }
        new IGridGameState<TMove, TResult> Move(TMove move);
    }
}