using System.Threading;
using System.Threading.Tasks;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// A pre-built set of chained functions that will be run in order
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Runs the functions syncronously 
        /// </summary>
        /// <param name="progress">Used to give progress feedback from within the pipeline</param>
        void Run(IProgressTracker progress = null);
    }

    /// <summary>
    /// A pre-built set of chained functions that will be run in order
    /// </summary>
    /// <typeparam name="TInput">Required input to the first function in the chain</typeparam>
    public interface IPipeline<TInput>
    {
        /// <summary>
        /// Runs the functions syncronously 
        /// </summary>
        /// <param name="input">Required input to the first function in the chain></param>
        /// <param name="progress">Used to give progress feedback from within the pipeline</param>
        void Run(TInput input, IProgressTracker progress = null);
    }
}
