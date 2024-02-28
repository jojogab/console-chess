using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;

namespace ConsoleChess.chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "Q";
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

            // Above
            pos.setPosition(Position.Line - 1, Position.Column);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line - 1, pos.Column);
            }

            // North East
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

            // Right
            pos.setPosition(Position.Line, Position.Column + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line, pos.Column + 1);
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

            //Below
            pos.setPosition(Position.Line + 1, Position.Column);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line + 1, pos.Column);
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

            //Left
            pos.setPosition(Position.Line, Position.Column - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setPosition(pos.Line, pos.Column - 1);
            }

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

            return mat;
        }
    }
}
