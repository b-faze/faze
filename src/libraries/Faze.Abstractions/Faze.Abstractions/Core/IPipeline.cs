namespace Faze.Abstractions.Core
{
    public interface IPipeline<TInput>
    {
        void Run(TInput input);
    }

    public interface IPipeline
    {
        void Run();
    }
}
