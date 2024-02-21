using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.Board
{
    internal class Board
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int line, int column)
        {
            Line = line;
            Column = column;
            Pieces = new Piece[Line, Column];
        }
    }
}
