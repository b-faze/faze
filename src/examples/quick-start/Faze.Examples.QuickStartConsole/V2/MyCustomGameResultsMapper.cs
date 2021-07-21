using Faze.Abstractions.Core;
using Faze.Abstractions.GameMoves;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.GameStates;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;

namespace Faze.Examples.QuickStartConsole.V2
{
    public class MyCustomGameResultsMapper : ITreeMapper<IGameState<GridMove, WinLoseDrawResult?>, WinLoseDrawResultAggregate>
    {
        public Tree<WinLoseDrawResultAggregate> Map(Tree<IGameState<GridMove, WinLoseDrawResult?>> tree, IProgressTracker progress)
        {
            return tree
                .MapValue(state => state.GetResult())
                .MapValueAgg(result =>
                {
                    return result.HasValue
                        ? new WinLoseDrawResultAggregate(result.Value)
                        : new WinLoseDrawResultAggregate();
                }, () => new WinLoseDrawResultAggregate());
        }
    }
}
