using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Core.Serialisers
{
    public class WinLoseDrawResultAggregateSerialiser : IValueSerialiser<WinLoseDrawResultAggregate>
    {
        public WinLoseDrawResultAggregate Deserialize(string valueString)
        {
            var values = valueString.Split(',').Select(uint.Parse).ToArray();
            return new WinLoseDrawResultAggregate(values[0], values[1], values[2]);
        }

        public string Serialize(WinLoseDrawResultAggregate value)
        {
            return $"{value.Wins},{value.Loses},{value.Draws}";
        }
    }
}
