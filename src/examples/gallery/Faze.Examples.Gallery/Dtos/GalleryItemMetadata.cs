using Newtonsoft.Json;

namespace Faze.Examples.Gallery
{
    public class GalleryItemMetadata
    {
        [JsonProperty("fileId")]
        public string FileId { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("pipelineId")]
        public string PipelineId { get; set; }

        [JsonProperty("variation")]
        public string Variation { get; set; }

        [JsonProperty("depth")]
        public int Depth { get; set; }
    }

    public class GalleryItemMetadata<TConfig> : GalleryItemMetadata
    {
        [JsonProperty("config")]
        public TConfig Config { get; set; }
    }
}
