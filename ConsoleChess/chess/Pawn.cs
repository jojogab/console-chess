using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace ConsoleChess.chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(color, board) 
        {
            Match = match;
        }

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

                //#SpecialMove En Passant
                if(Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if(Board.validPosition(left) && existOpponent(left) && Board.piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.validPosition(right) && existOpponent(right) && Board.piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
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

                //#SpecialMove En Passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.validPosition(left) && existOpponent(left) && Board.piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.validPosition(right) && existOpponent(right) && Board.piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
