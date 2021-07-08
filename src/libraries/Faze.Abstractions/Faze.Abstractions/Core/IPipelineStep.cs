namespace Faze.Abstractions.Core
{
    public interface IPipelineStep
    {
        object Execute(object input);
    }
}
