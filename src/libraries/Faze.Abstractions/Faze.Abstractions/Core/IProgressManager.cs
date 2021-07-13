namespace Faze.Abstractions.Core
{
    public interface IProgressManager
    {
        IProgressBar Start(int totalTicks, string message);
    }
}
