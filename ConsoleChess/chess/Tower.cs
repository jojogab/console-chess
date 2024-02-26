using ConsoleChess.board;
using ConsoleChess.board.Enums;

namespace ConsoleChess.chess
{
    internal class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "T";
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
            while(Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if(Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            // Below
            pos.setPosition(Position.Line + 1, Position.Column);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line = pos.Line + 1;
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
                pos.Column = pos.Column + 1;
            }

            // Left
            pos.setPosition(Position.Line, Position.Column - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
