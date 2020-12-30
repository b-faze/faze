using System.Net;

namespace Faze.Abstractions.Rendering
{
    public interface IViewPort
    {
        float Left { get; }
        float Top { get; }
        float Scale { get; }
    }

    public struct ViewPort : IViewPort
    {
        public ViewPort(float left, float top, float scale)
        {
            Left = left;
            Top = top;
            Scale = scale;
        }

        public static ViewPort Default()
        {
            return new ViewPort(0, 0, 1);
        }

        /// <summary>
        /// Relative position as a fraction of the image's width
        /// </summary>
        public float Left { get; }

        /// <summary>
        /// Relative position as a fraction of the image's height
        /// </summary>
        public float Top { get; }

        /// <summary>
        /// Relative scale of the viewport compared to the image's size
        /// e.g. Scale = 0.5 would be a view port half the size of the original image
        /// </summary>
        public float Scale { get; }
    }
}
