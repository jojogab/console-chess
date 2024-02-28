using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;

namespace ConsoleChess.chess
{
    internal class Horse : Piece
    {
        public Horse(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "H";
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

            pos.setPosition(Position.Line - 1, Position.Column - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line - 2, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line - 2, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line - 1, Position.Column + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line + 1, Position.Column + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line + 2, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line + 2, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.setPosition(Position.Line + 1, Position.Column - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
