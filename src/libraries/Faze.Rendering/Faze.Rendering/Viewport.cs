using System;

namespace Faze.Abstractions.Rendering
{
    internal class Viewport : IViewport
    {
        public Viewport(float left, float top, float scale)
        {
            Left = Bound(left);
            Top = Bound(top);
            Scale = scale;
        }

        public static Viewport Default()
        {
            return new Viewport(0, 0, 1);
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

        public IViewport Zoom(float x, float y, float newScale)
        {
            var ds = newScale - Scale;
            var dx = -x * ds;
            var dy = -y * ds;

            return new Viewport(Left + dx, Top + dy, newScale);
        }

        public IViewport Pan(float dx, float dy)
        {
            return new Viewport(Left + dx, Top + dy, Scale);
        }

        public IViewport Tween(float x, float y, float newScale, float f)
        {
            var ds = newScale - Scale;
            var dx = x - Left;
            var dy = y - Top;

            return new Viewport(Left + f * dx, Top + f * dy, Scale + f * ds);
        }

        private float Bound(float n) => Math.Min(1, Math.Max(0, n));
    }
}
