namespace Faze.Core.Pipelines
{
    internal interface IPipelineStep
    {
        object Execute(object input);
    }
}
