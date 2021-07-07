using Faze.Abstractions.Rendering;
using System;
using System.IO;

namespace Faze.Examples.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly GalleryServiceConfig config;

        public GalleryService(GalleryServiceConfig config)
        {
            this.config = config;
        }

        public void Save(IPaintedTreeRenderer renderer, GalleryItemMetadata data)
        {
            var filePath = GetFilePath(data);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fs = File.OpenWrite(filePath))
            {
                renderer.Save(fs);
            }
        }

        private string GetFilePath(GalleryItemMetadata data)
        {
            var relativePath = Path.Combine(data.Albums);
            
            return Path.Combine(config.BasePath, relativePath, data.FileName);
        }
    }
}
