using Newtonsoft.Json;

namespace Faze.Examples.Gallery
{
    public class GalleryPipelineMetadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("dataId")]
        public string DataId { get; set; }

        [JsonProperty("relativeCodePath")]
        public string RelativeCodePath { get; set; }
    }
}
