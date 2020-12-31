using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    public interface IPaintedTreeRenderer
    {
        Tree<T> GetVisible<T>(Tree<T> tree, Viewport viewPort);
        void Draw(Tree<Color> tree, Viewport viewPort, int? maxDepth = null);
        Bitmap GetBitmap();
    }
}
