using ShellProgressBar;
using System;
using IProgressTracker = Faze.Abstractions.Core.IProgressTracker;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public class ProgressBarWrapper : IProgressTracker
    {
        private readonly DateTime startTime;
        private readonly ShellProgressBar.IProgressBar progressBar;

        public ProgressBarWrapper(ShellProgressBar.IProgressBar progressBar)
        {
            this.startTime = DateTime.Now;
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
            if (progressBar is ProgressBarBase b) {
                var estimatedSeconds = (DateTime.Now - startTime).TotalSeconds / b.Percentage * 100;
                b.EstimatedDuration = TimeSpan.FromSeconds(estimatedSeconds);
            }
        }

        public IProgressTracker Spawn()
        {
            var childOptions = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Green,
                BackgroundColor = ConsoleColor.DarkGreen,
                ProgressCharacter = '─',
                CollapseWhenFinished = true,
                ShowEstimatedDuration = true
            };

            return new ProgressBarWrapper(progressBar.Spawn(100, "", childOptions));
        }

        public void Dispose()
        {
            progressBar.Dispose();
        }
    }
}
