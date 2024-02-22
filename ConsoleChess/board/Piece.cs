using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChess.board.Enums;

namespace ConsoleChess.board
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QntMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece( Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            QntMoves = 0;
        }

        public void increasemoves()
        {
            QntMoves++;
        }
    }
}
