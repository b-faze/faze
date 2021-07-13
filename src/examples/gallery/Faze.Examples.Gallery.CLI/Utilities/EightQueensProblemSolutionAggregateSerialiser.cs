using Faze.Abstractions.Core;
using Faze.Examples.Gallery.CLI.Visualisations.PieceBoards.DataGenerators;
using System.Linq;

namespace Faze.Examples.Gallery.CLI.Utilities
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
