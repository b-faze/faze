using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.Rubik
{
    internal class RubikFaceSide
    {
        public static RubikFaceSide Initial(RubikColor color) => new RubikFaceSide(new List<RubikColor> { color, color, color });

        private RubikFaceSide(IEnumerable<RubikColor> side)
        {
            Value = side.ToList();
        }

        public IEnumerable<RubikColor> Value { get; }
    }
}
