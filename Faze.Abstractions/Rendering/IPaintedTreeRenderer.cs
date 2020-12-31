using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    public interface IPaintedTreeRenderer
    {
        Tree<T> GetVisible<T>(Tree<T> tree, IViewport viewPort);
        void Draw(Tree<Color> tree, IViewport viewPort, int? maxDepth = null);
        Bitmap GetBitmap();
    }
}
