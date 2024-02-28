using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;

namespace ConsoleChess.chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "P";
        }
        private bool existOpponent(Position pos)
        {
            Piece p = Board.piece(pos);
            return p != null && p.Color != Color;
        }
        private bool freeSpace(Position pos)
        {
            return Board.piece(pos) == null;
        }
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                pos.setPosition(Position.Line - 1, Position.Column);
                if (Board.validPosition(pos) && freeSpace(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line - 2, Position.Column);
                if (Board.validPosition(pos) && freeSpace(pos) && QntMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line - 1, Position.Column - 1);
                if (Board.validPosition(pos) && existOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line - 1, Position.Column + 1);
                if (Board.validPosition(pos) && existOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }
            else
            {
                pos.setPosition(Position.Line + 1, Position.Column);
                if (Board.validPosition(pos) && freeSpace(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line + 2, Position.Column);
                if (Board.validPosition(pos) && freeSpace(pos) && QntMoves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line + 1, Position.Column - 1);
                if (Board.validPosition(pos) && existOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.setPosition(Position.Line + 1, Position.Column + 1);
                if (Board.validPosition(pos) && existOpponent(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }

            return mat;
        }
    }
}
