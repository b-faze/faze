using System.Threading;
using System.Threading.Tasks;

namespace Faze.Abstractions.Core
{
    public interface IPipeline
    {
        void Run(IProgressTracker progress = null);
    }

    public interface IPipeline<TInput>
    {
        void Run(TInput input, IProgressTracker progress = null);
    }
}
