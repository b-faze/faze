namespace Faze.Abstractions.Core
{
    public interface IPipelineStepProgress : IPipelineStep
    {
        object Execute(object input, IProgressBar progress);
    }
}
