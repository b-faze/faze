using System;

namespace Faze.Abstractions.Core
{
    public interface IProgressBar : IDisposable
    {
        void SetMaxTicks(int ticks);
        void Tick();
        IProgressBar Spawn();
        void SetMessage(string message);
    }
}
