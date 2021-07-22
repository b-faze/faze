using Faze.Abstractions.Rendering;
using System;

namespace Faze.Rendering.Playground
{
    public class Options 
    {
        public int Size { get; set; }
        public int RenderDepth { get; set; }
        public float Border { get; set; }
        public float MinChildDrawSize { get; set; }
        public ViewportJson ViewportJson { get; set; }

        public IViewport DefaultViewport 
        { 
            get
            {
                throw new NotImplementedException();
            }
        }


        //public IViewport DefaultViewport => ViewportJson != null
        //    ? new Viewport(ViewportJson.Left, ViewportJson.Top, ViewportJson.Scale)
        //    : Viewport.Default();
    }
}
