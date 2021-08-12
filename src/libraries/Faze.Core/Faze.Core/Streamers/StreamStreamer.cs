using Faze.Abstractions.Core;
using System.IO;

namespace Faze.Core.Streamers
{
    internal class StreamStreamer : IStreamer
    {
        private readonly Stream baseStream;

        public StreamStreamer(Stream stream)
        {
            this.baseStream = stream;
        }
        public void WriteToStream(Stream stream)
        {
            baseStream.CopyTo(stream);
        }
    }
}
