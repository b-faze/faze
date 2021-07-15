namespace Faze.Engine.Players
{
    public interface IResultEvaluator<TResult>
    {
        TResult MinValue { get; }
        TResult MaxValue { get; }
        int Compare(TResult a, TResult b);
    }
}
