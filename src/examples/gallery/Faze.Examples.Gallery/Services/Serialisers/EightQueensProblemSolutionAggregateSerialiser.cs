using Faze.Abstractions.Core;
using Faze.Examples.Gallery.Services.Aggregates;
using System.Linq;

namespace Faze.Examples.Gallery.Services.Serialisers
{
    public class EightQueensProblemSolutionAggregateSerialiser : IValueSerialiser<EightQueensProblemSolutionAggregate>
    {
        public EightQueensProblemSolutionAggregate Deserialize(string valueString)
        {
            var values = valueString.Split(',').Select(uint.Parse).ToArray();
            return new EightQueensProblemSolutionAggregate(values[0], values[1]);
        }

        public string Serialize(EightQueensProblemSolutionAggregate value)
        {
            return $"{value.Wins},{value.Loses}";
        }
    }
}
