using Faze.Abstractions.Core;
using SkiaSharp;
using System.IO;

namespace Faze.Rendering.TreeRenderers
{
    public abstract class SkiaTreeRenderer : IStreamer
    {
        protected readonly SKSurface surface;

        public SkiaTreeRenderer(int size)
        {
            this.surface = SKSurface.Create(new SKImageInfo(size, size));
        }

        public void WriteToStream(Stream stream)
        {
            using SKImage image = surface.Snapshot();
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);

            data.AsStream().CopyTo(stream);
        }
    }
}
