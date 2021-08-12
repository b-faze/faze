﻿using Faze.Abstractions.Core;
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
        public void WriteToStream(Stream stream)
        {
            foreach (var streamer in streamers)
            {
                streamer.WriteToStream(stream);
            }
        }
    }
}