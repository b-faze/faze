namespace Faze.Abstractions.GameStates
{
    public interface IGameResult<out TResult>
    {
        TResult GetResult();
    }
}