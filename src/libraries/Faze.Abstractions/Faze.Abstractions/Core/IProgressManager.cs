namespace Faze.Abstractions.Core
{
    public interface IProgressManager
    {
        IProgressTracker Start(int totalTicks, string message);
    }
}
