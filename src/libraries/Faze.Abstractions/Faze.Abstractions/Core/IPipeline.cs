namespace Faze.Abstractions.Core
{
    public interface IPipeline<TInput>
    {
        void Run(TInput input);
        void Run(TInput input, IProgressBar progress);
    }

    public interface IPipeline
    {
        void Run();
        void Run(IProgressBar progress);
    }
}
