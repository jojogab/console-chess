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
    }
}
