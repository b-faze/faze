using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    public interface IPaintedTreeRenderer
    {
        Bitmap Draw(IPaintedTree tree, int size, int? maxDepth = null);
    }
}
