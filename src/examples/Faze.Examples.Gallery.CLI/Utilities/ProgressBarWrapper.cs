using IProgressBar = Faze.Abstractions.Core.IProgressBar;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public class ProgressBarWrapper : IProgressBar
    {
        private readonly ShellProgressBar.IProgressBar progressBar;

        public ProgressBarWrapper(ShellProgressBar.IProgressBar progressBar)
        {
            this.progressBar = progressBar;
        }

        public void SetMaxTicks(int ticks)
        {
            progressBar.MaxTicks = ticks;
        }

        public void SetMessage(string message)
        {
            progressBar.Message = message;
        }

        public void Tick()
        {
            progressBar.Tick();
        }

        public IProgressBar Spawn()
        {
            return new ProgressBarWrapper(progressBar.Spawn(100, ""));
        }

        public void Dispose()
        {
            progressBar.Dispose();
        }
    }
}
