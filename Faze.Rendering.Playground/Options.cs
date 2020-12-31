using Faze.Abstractions.Rendering;

namespace Faze.Rendering.Playground
{
    public class Options 
    {
        public int Size { get; set; }
        public int RenderDepth { get; set; }
        public float Border { get; set; }
        public ViewportJson ViewportJson { get; set; }
        public Viewport DefaultViewport => ViewportJson != null
            ? new Viewport(ViewportJson.Left, ViewportJson.Top, ViewportJson.Scale)
            : Viewport.Default();
    }
}
