namespace Faze.Abstractions.GameStates
{
    /// <summary>
    /// Represents a type which can provide a result
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IGameResult<out TResult>
    {
        TResult GetResult();
    }
}