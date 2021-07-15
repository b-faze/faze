using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Abstractions.Players
{
    public interface IMoveDistribution<TMove>
    {
        /// <summary>
        /// Gets a move across the distribution
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        TMove GetMove(UnitInterval ui);

        /// <summary>
        /// True if there are no moves to choose from
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}