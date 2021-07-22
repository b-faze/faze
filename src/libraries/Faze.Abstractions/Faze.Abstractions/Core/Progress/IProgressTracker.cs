using System;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Represents a tracker for a given task
    /// </summary>
    public interface IProgressTracker : IDisposable
    {
        void SetMaxTicks(int ticks);
        void SetMessage(string message);

        void Tick();

        /// <summary>
        /// Creates a new instance of an IProgressTracker for tracking sub-tasks
        /// </summary>
        /// <returns></returns>
        IProgressTracker Spawn();
    }
}
