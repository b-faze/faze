using Faze.Abstractions.Core;

namespace Faze.Utilities.Testing
{
    public class TestProgressTracker : IProgressTracker
    {
        public void Dispose()
        {
            // do nothing
        }

        public void SetMaxTicks(int ticks)
        {
            MaxTicks = ticks;
        }

        public void SetMessage(string message)
        {
            // do nothing
        }

        public IProgressTracker Spawn()
        {
            return new TestProgressTracker();
        }

        public void Tick()
        {
            Ticks++;
        }

        public int Ticks { get; private set; }
        public int MaxTicks { get; private set; }
    }
}
