using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.GameMoves
{
    public struct GridMove
    {
        private int index;

        public GridMove(int index)
        {
            this.index = index;
        }

        public GridMove(int x, int y, int width)
        {
            this.index = x + y * width;
        }

        public static implicit operator int(GridMove playerIndex)
        {
            return playerIndex.index;
        }

        public static implicit operator GridMove(int index)
        {
            return new GridMove(index);
        }
    }
}
