using Newtonsoft.Json;
using System.Collections.Generic;

namespace Faze.Examples.Gallery
{
    public class GallerySiteSettings
    {
        /// <summary>
        /// Gallery item filename -> metadata mapping
        /// </summary>
        [JsonProperty("itemMetadata")]
        public Dictionary<string, GalleryItemMetadata> ItemMetadata { get; set; }
    }
}
