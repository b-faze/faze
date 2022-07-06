using System.Collections.Generic;
using System.Linq;

namespace Faze.Examples.Games.Rubik
{
    public static class RubikUtils
    {
        private const char MoveSequenceSeparator = ' ';

        public static IEnumerable<RubikMove> ParseMoves(string moveSequence)
        {
            return moveSequence.Split(MoveSequenceSeparator).Select(RubikMove.Parse);
        }
    }
}
