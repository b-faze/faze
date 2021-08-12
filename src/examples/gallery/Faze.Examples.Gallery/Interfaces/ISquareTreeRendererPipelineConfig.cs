namespace Faze.Examples.Gallery.Interfaces
{
    public interface ISquareTreeRendererPipelineConfig
    {
        int TreeSize { get; set; }
        int ImageSize { get; set; }
        float BorderProportion { get; set; }
        int MaxDepth { get; set; }
    }
}
