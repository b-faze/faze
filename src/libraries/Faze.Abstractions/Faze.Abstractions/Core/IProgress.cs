using System;

namespace Faze.Abstractions.Core
{
    public class Progress
    {
        private readonly int total;

        public Progress(int total)
        {
            this.total = total;
        }
    }

    public interface IProgressManager
    {
        IProgressBar Start(int totalTicks, string message);
    }

    public interface IProgressBar : IDisposable
    {
        void SetMaxTicks(int ticks);
        void Tick();
        IProgressBar Spawn();
        void SetMessage(string message);
    }
}
