using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Faze.Rendering.Extensions
{
    public static class PaintedTreeRendererExtensions
    {
        public static void Save(this IPaintedTreeRenderer renderer, string filename)
        {
            using var fs = File.OpenWrite(filename);

            renderer.Save(fs);
        }
    }
}
