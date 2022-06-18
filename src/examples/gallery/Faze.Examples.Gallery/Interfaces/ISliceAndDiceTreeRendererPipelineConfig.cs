namespace Faze.Examples.Gallery.Interfaces
{
    public interface ISliceAndDiceTreeRendererPipelineConfig
    {
        int ImageSize { get; set; }
        float BorderProportion { get; set; }
        int MaxDepth { get; set; }
    }
}
