using Faze.Abstractions.Core;

namespace Faze.Abstractions.Rendering
{
    public interface ITreePainter
    {
        PaintedTree Paint<T>(Tree<T> tree);
    }

    public interface ITreePainter<TValue>
    {
        PaintedTree Paint(Tree<TValue> tree);
    }
}
