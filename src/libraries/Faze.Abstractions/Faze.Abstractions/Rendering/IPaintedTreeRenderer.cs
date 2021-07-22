using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    /// <summary>
    /// Draws a tree that has had its nodes colored
    /// </summary>
    public interface IPaintedTreeRenderer
    {
        Tree<T> GetVisible<T>(Tree<T> tree);
        void Draw(Tree<Color> tree);
        void Save(Stream stream);
    }
}
