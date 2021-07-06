﻿/*
ChessLib, a chess data structure library

MIT License

Copyright (c) 2017-2020 Rudy Alex Kohn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Rudz.Chess.Tables
{
    using Enums;
    using Extensions;
    using Types;

    public sealed class HistoryHeuristic : IHistoryHeuristic
    {
        private readonly int[][][] _table;

        public HistoryHeuristic()
        {
            _table = new int[(int)Players.PlayerNb][][];
            Initialize(Player.White);
            Initialize(Player.Black);
        }

        public void Clear()
        {
            ClearTable(Player.White);
            ClearTable(Player.Black);
        }

        public void Set(Player c, Square from, Square to, int value)
            => _table[c.Side][from.AsInt()][to.AsInt()] = value;

        public int Retrieve(Player c, Square from, Square to)
            => _table[c.Side][from.AsInt()][to.AsInt()];

        private void Initialize(Player c)
        {
            _table[c.Side] = new int[64][];
            for (var i = 0; i < _table[c.Side].Length; ++i)
                _table[c.Side][i] = new int[64];
        }

        private void ClearTable(Player c)
        {
            for (var i = 0; i < _table[c.Side].Length; i++)
                _table[c.Side][i].Clear();
        }
    }
}