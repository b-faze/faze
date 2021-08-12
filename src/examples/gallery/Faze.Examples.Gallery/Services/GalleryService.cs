using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System.IO;

namespace Faze.Examples.Gallery.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly GalleryServiceConfig config;

        public GalleryService(GalleryServiceConfig config)
        {
            this.config = config;
        }

        public void Save(IStreamer streamer, GalleryItemMetadata data)
        {
            var filePath = GetImageFilename(data);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fs = File.OpenWrite(filePath))
            {
                streamer.WriteToStream(fs);
            }
        }

        public string GetImageFilename(GalleryItemMetadata data)
        {
            var filename = Path.Combine(config.ImageBasePath, data.Album, data.FileName);

            var directory = Path.GetDirectoryName(Path.GetFullPath(filename));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return filename;
        }

        public string GetDataFilename(string id)
        {
            var filename = Path.Combine(config.DataBasePath, id);

            var directory = Path.GetDirectoryName(Path.GetFullPath(filename));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return filename;
        }
    }
}
