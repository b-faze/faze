using System;
using System.Windows.Forms;

namespace Faze.Rendering.Playground
{
    internal class RectRecursiveTemplateChildArea : RecursiveTemplateChildArea
    {
        private const float ResizeR = 0.01f;

        public RectRecursiveTemplateChildArea(float left, float top, float right, float bottom) : base(Guid.NewGuid().ToString())
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public bool IsRegular { get; private set; }
        public float Left { get; private set; }
        public float Top { get; private set; }
        public float Right { get; private set; }
        public float Bottom { get; private set; }

        internal override bool Intercepts(float x, float y)
        {
            return x >= Left && x <= Right
                && y >= Top && y <= Bottom;
        }

        internal override void Move(float x, float y)
        {
            var width = Right - Left;
            var height = Bottom - Top;

            Left = x;
            Top = y;
            Right = x + width;
            Bottom = y + height;
        }

        internal override (float x, float y) GetMovePoint()
        {
            return (Left, Top);
        }

        internal override bool WithinResizeUI(float x, float y)
        {
            return Math.Abs(x - Left) <= ResizeR && y >= Top && y <= Bottom
                || Math.Abs(x - Right) <= ResizeR && y >= Top && y <= Bottom
                || Math.Abs(y - Top) <= ResizeR && x >= Left && x <= Right
                || Math.Abs(y - Bottom) <= ResizeR && x >= Left && x <= Right;
        }

        internal override Cursor GetResizeCursor(float x, float y)
        {
            var leftRight = Math.Abs(x - Left) <= ResizeR && y >= Top && y <= Bottom
                || Math.Abs(x - Right) <= ResizeR && y >= Top && y <= Bottom;

            var topBottom = Math.Abs(y - Top) <= ResizeR && x >= Left && x <= Right
                || Math.Abs(y - Bottom) <= ResizeR && x >= Left && x <= Right;

            if (leftRight)
                return Cursors.SizeWE;

            if (topBottom)
                return Cursors.SizeNS;

            return Cursors.Default;
        }
    }
}
