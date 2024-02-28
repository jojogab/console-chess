using ConsoleChess.board;
using ConsoleChess.board.Enums;
using System.Numerics;

namespace ConsoleChess.chess
{
    internal class King : Piece
    {
        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.piece(pos);
            return p == null || p.Color != Color;

        }

        private bool testTowerForRoque(Position pos)
        {
            Piece p = Board.piece(pos);
            return p != null && p is Tower && p.Color == Color && p.QntMoves == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // Above
            pos.setPosition(Position.Line - 1, Position.Column);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // North East
            pos.setPosition(Position.Line - 1, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // Right
            pos.setPosition(Position.Line, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //South East
            pos.setPosition(Position.Line + 1, Position.Column + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //Below
            pos.setPosition(Position.Line + 1, Position.Column);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //South West
            pos.setPosition(Position.Line + 1, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //Left
            pos.setPosition(Position.Line, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //North West
            pos.setPosition(Position.Line - 1, Position.Column - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }


            //#SpecialMove Roque
            if(QntMoves == 0 && !Match.Check)
            {
                //#SpecialMove Little Roque
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (testTowerForRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.piece(p1) == null && Board.piece(p2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //#SpecialMove Big Roque
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (testTowerForRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.piece(p1) == null && Board.piece(p2) == null && Board.piece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }


            return mat;

        }
    }
}
