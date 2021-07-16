using ShellProgressBar;
using System;
using IProgressTracker = Faze.Abstractions.Core.IProgressTracker;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public class ProgressBarWrapper : IProgressTracker
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

        public IProgressTracker Spawn()
        {
            var childOptions = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Green,
                BackgroundColor = ConsoleColor.DarkGreen,
                ProgressCharacter = '─',
                CollapseWhenFinished = true
            };

            return new ProgressBarWrapper(progressBar.Spawn(100, "", childOptions));
        }

        public void Dispose()
        {
            progressBar.Dispose();
        }
    }
}
