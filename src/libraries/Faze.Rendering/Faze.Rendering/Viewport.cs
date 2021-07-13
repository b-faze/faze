namespace Faze.Abstractions.Rendering
{
    public class Viewport : IViewport
    {
        public Viewport(float left, float top, float scale)
        {
            Left = left;
            Top = top;
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
            var scaleChange = newScale - Scale;
            var dx = -x * scaleChange;
            var dy = -y * scaleChange;

            return new Viewport(Left + dx, Top + dy, newScale);
        }

        public IViewport Pan(float dx, float dy)
        {
            return new Viewport(Left + dx, Top + dy, Scale);
        }
    }
}
