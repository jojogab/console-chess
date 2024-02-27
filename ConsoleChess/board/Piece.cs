using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleChess.board.Enums;

namespace ConsoleChess.board
{
    internal abstract class Piece
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

        public abstract bool[,] possibleMoves();

        public void increasemoves()
        {
            QntMoves++;
        }

        public void decreasemoves()
        {
            QntMoves--;
        }

        public bool existPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return possibleMoves()[pos.Line, pos.Column];
        }
    }
}
