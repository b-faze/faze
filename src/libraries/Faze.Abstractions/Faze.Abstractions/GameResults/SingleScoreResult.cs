using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.GameResults
{
    public struct SingleScoreResult
    {
        private int score;

        public SingleScoreResult(int score)
        {
            this.score = score;
        }

        public static implicit operator int(SingleScoreResult result) => result.score;
    }
}
