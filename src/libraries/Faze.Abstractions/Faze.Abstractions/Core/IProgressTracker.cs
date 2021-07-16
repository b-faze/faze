using System;

namespace Faze.Abstractions.Core
{
    public interface IProgressTracker : IDisposable
    {
        void SetMaxTicks(int ticks);
        void SetMessage(string message);

        void Tick();
        IProgressTracker Spawn();
    }
}
