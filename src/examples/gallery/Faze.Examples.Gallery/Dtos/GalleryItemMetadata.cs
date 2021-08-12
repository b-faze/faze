using Faze.Examples.Gallery.Visualisations.EightQueensProblem;

namespace Faze.Examples.Gallery
{
    public class GalleryItemMetadata
    {
        public string FileName { get; set; }
        public string Album { get; set; }
        public string PipelineId { get; set; }
        public int Variation { get; set; }
        public int Depth { get; set; }
    }

    public class GalleryItemMetadata<TConfig> : GalleryItemMetadata
    {
        public TConfig Config { get; set; }
    }
}
