using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using System;
using System.Drawing;

namespace Faze.Rendering.TreePainters
{
    public class CheckeredTreePainter : ITreePainter
    {
        public Tree<Color> Paint<T>(Tree<T> tree)
        {
            return tree
                .MapValue((v, info) => info.ChildIndex % 2 == 0 ? Color.Black : Color.White);
        }
    }
}
