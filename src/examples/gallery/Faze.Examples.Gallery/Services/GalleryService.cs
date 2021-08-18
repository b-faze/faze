using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Examples.Gallery.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Faze.Examples.Gallery.Extensions;

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
            var filePath = GetItemFilename(data);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fs = File.OpenWrite(filePath))
            {
                streamer.WriteToStream(fs);
            }

            //fileMetadataService.ApplyMetadata(filePath, data);
        }

        public string GetItemFilename(GalleryItemMetadata data)
        {
            var filename = Path.Combine(config.ImageBasePath, data.Album, data.PipelineId, data.Variation, data.FileId);

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
