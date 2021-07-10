using Faze.Abstractions.Core;
using ShellProgressBar;
using System;
using System.Threading.Tasks;
using IProgressBar = Faze.Abstractions.Core.IProgressBar;

namespace Faze.Examples.Gallery.CLI.Utilities
{
    public class GalleryProgressManager : IProgressManager
    {
        public IProgressBar Start(int totalTicks, string message)
        {
            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                BackgroundColor = ConsoleColor.DarkYellow,
                ProgressCharacter = '─',
            };

            return new ProgressBarWrapper(new ProgressBar(totalTicks, message, options));

        }
    }
}
