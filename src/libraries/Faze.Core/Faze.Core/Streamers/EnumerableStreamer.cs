using Faze.Abstractions.Core;
using System.Collections.Generic;
using System.IO;

namespace Faze.Core.Streamers
{
    internal class EnumerableStreamer : IStreamer
    {
        private readonly IEnumerable<IStreamer> streamers;

        public EnumerableStreamer(IEnumerable<IStreamer> streamers)
        {
            this.streamers = streamers;
        }
        public void WriteToStream(Stream stream, IProgressTracker progress = null)
        {
            progress = progress ?? NullProgressTracker.Instance;

            var i = 0;
            foreach (var streamer in streamers)
            {
                progress.SetMessage($"Streaming #{i++}...");
                streamer.WriteToStream(stream);
            }
        }
    }
}
