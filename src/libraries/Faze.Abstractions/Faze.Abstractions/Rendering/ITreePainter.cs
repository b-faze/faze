using Faze.Abstractions.Core;
using System.Drawing;

namespace Faze.Abstractions.Rendering
{
    /// <summary>
    /// Paints an arbitrary tree
    /// </summary>
    public interface ITreePainter
    {
        Tree<Color> Paint<T>(Tree<T> tree);
    }

    /// <summary>
    /// Paints a tree with a value of tyoe <typeparamref name="TValue"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ITreePainter<TValue>
    {
        Tree<Color> Paint(Tree<TValue> tree);
    }
}
