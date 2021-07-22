namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Used to manage progress trackers
    /// </summary>
    public interface IProgressManager
    {
        /// <summary>
        /// Creates a new instance of IProgressTracker
        /// </summary>
        /// <param name="totalTicks">Sets the maxTicks of the IProgressTracker</param>
        /// <param name="message">Sets the message of the IProgressTracker</param>
        /// <returns>IProgressTracker</returns>
        IProgressTracker Start(int totalTicks, string message);
    }
}
