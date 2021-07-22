using Faze.Abstractions.Core;

namespace Faze.Abstractions.Rendering
{
    /// <summary>
    /// Allows defining reusable tree mappers
    /// </summary>
    /// <typeparam name="TValueIn"></typeparam>
    /// <typeparam name="TValueOut"></typeparam>
    public interface ITreeMapper<TValueIn, TValueOut>
    {
        Tree<TValueOut> Map(Tree<TValueIn> tree, IProgressTracker progress);
    }
}
