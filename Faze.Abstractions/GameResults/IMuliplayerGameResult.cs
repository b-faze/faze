using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.GameResults
{
    public interface IMuliplayerGameResult<TResult>
    {
        TResult ResultFor<TPlayer>(TPlayer player);
    }
}
