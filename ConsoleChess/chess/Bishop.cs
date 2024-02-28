using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;

namespace ConsoleChess.chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "B";
        }
        private bool canMove(Position pos)
        {
            Piece p = Board.piece(pos);
            return p == null || p.Color != Color;
        }
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //North West
            pos.setPosition(Position.Line - 1, Position.Column - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line - 1, pos.Column - 1);
            }

            //North East
            pos.setPosition(Position.Line - 1, Position.Column + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line - 1, pos.Column + 1);
            }

            //South East
            pos.setPosition(Position.Line + 1, Position.Column + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line + 1, pos.Column + 1);
            }

            //South West
            pos.setPosition(Position.Line + 1, Position.Column - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
