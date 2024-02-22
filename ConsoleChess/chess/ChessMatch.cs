using System;
using ConsoleChess.board;
using ConsoleChess.board.Enums;

namespace ConsoleChess.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn { get; set; }
        private Color AtualPlayer { get; set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            AtualPlayer = Color.White;
            Finished = false;
            putPieces();
        }

        public void executemove(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.increasemoves();
            Piece capturedpiece =  Board.removePiece(destiny);
            Board.putPiece(p, destiny);
        }

        private void putPieces()
        {
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).toPosition());
        }
    }
}
