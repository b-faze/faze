using System.IO;

namespace Faze.Abstractions.Core
{
    public interface IStreamer
    {
        void WriteToStream(Stream stream, IProgressTracker progress = null);
    }
}
