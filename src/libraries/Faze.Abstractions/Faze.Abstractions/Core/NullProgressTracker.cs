namespace Faze.Abstractions.Core
{
    public class NullProgressTracker : IProgressTracker
    {
        private NullProgressTracker() { }

        public static readonly NullProgressTracker Instance = new NullProgressTracker();

        public void Dispose()
        {
            // do nothing
        }

        public void SetMaxTicks(int ticks)
        {
            // do nothing
        }

        public void SetMessage(string message)
        {
            // do nothing
        }

        public IProgressTracker Spawn()
        {
            return this;
        }

        public void Tick()
        {
            // do nothing
        }
    }
}
