namespace Faze.Abstractions.Core
{
    public interface IPipeline<TInput>
    {
        void Run(TInput input);
    }
}
