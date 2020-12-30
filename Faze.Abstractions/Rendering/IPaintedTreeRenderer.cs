using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    public interface IPaintedTreeRenderer
    {
        Tree<T> GetVisible<T>(Tree<T> tree, IViewPort viewPort);
        Bitmap Draw(Tree<Color> tree, int size, int? maxDepth = null);
    }

    public interface IPaintedTreeRenderer<TCanvas> : IPaintedTreeRenderer
    {
        TCanvas Draw(Tree<Color> tree, IViewPort viewPort, int? maxDepth = null);
    }
}
