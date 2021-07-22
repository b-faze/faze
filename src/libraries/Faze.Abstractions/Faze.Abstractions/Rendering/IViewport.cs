using System;

namespace Faze.Abstractions.Rendering
{
    /// <summary>
    /// Used to keep track of the visible window.
    /// To be used by the renderer
    /// </summary>
    public interface IViewport
    {        
        /// <summary>
        /// Relative position as a fraction of the image's width
        /// </summary>
        float Left { get; }

        /// <summary>
        /// Relative position as a fraction of the image's height
        /// </summary>
        float Top { get; }

        /// <summary>
        /// Relative scale of the viewport compared to the image's size
        /// e.g. Scale = 0.5 would be a view port half the size of the original image
        /// </summary>
        float Scale { get; }

        IViewport Zoom(float x, float y, float newScale);
        IViewport Pan(float dx, float dy);

    }
}
