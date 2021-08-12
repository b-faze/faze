using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Faze.Core.Extensions
{
    public static class StreamerExtensions
    {
        public static void SaveToFile(this IStreamer renderer, string filename)
        {
            using (var fs = File.OpenWrite(filename))
            {
                renderer.WriteToStream(fs);
            }
        }
    }
}
