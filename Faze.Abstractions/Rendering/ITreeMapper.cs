using Faze.Abstractions.Core;

namespace Faze.Abstractions.Rendering
{
    public interface ITreeMapper<TValueIn, TValueOut>
    {
        Tree<TValueOut> Map(Tree<TValueIn> tree);
    }
}
