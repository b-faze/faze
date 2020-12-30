using Faze.Abstractions.Rendering;

namespace Faze.Rendering.Playground
{
    public class Options 
    {
        public int Size { get; set; }
        public int RenderDepth { get; set; }
        public float Border { get; set; }
        public ViewPortJson ViewPortJson { get; set; }
        public IViewPort DefaultViewPort => ViewPortJson != null
            ? new ViewPort(ViewPortJson.Left, ViewPortJson.Top, ViewPortJson.Scale)
            : ViewPort.Default();
    }
}
