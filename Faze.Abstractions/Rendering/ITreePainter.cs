using Faze.Abstractions.Core;
using System.Drawing;

namespace Faze.Abstractions.Rendering
{
    public interface ITreePainter
    {
        Tree<Color> Paint<T>(Tree<T> tree);
    }

    public interface ITreePainter<TValue>
    {
        Tree<Color> Paint(Tree<TValue> tree);
    }
}
