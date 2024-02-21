using ConsoleChess.board;
using ConsoleChess.board.Enums;

namespace ConsoleChess.chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "K";
        }
    }
}
