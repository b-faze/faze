using Faze.Abstractions.Core;

namespace Faze.Core.Pipelines
{
    internal interface IPipelineStepProgress : IPipelineStep
    {
        object Execute(object input, IProgressTracker progress);
    }
}
