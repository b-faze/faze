using Faze.Abstractions.GameStates;
using Faze.Abstractions.Players;
using Faze.Engine.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Engine.Players
{
    /// <summary>
    /// Simplest agent. Has no preference for the available moves
    /// </summary>
    public class MonkeyAgent : IPlayer
    {
        public IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state)
        {
            var availableMoves = state.GetAvailableMoves().ToArray();

            return new MoveDistribution<TMove>(availableMoves);
        }
    }
}
