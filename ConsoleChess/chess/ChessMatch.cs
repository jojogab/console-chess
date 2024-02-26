using System;
using ConsoleChess.board;
using ConsoleChess.board.Enums;
using ConsoleChess.board.Exceptions;

namespace ConsoleChess.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color AtualPlayer { get; private set; }
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

        public void makeMove(Position origin, Position destiny)
        {
            executemove(origin, destiny);
            Turn++;
            changePlayer();

        }

        public void validateOriginPosition(Position pos)
        {
            if(Board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the selected origin position!");
            }
            if(AtualPlayer != Board.piece(pos).Color)
            {
                throw new BoardException("The piece in the selected origin position is not your!");
            }
            if(!Board.piece(pos).existPossibleMoves())
            {
                throw new BoardException("There are no possible movements for this piece!");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.piece(origin).canMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void changePlayer()
        {
            if(AtualPlayer == Color.White)
            {
                AtualPlayer = Color.Black;
            }
            else
            {
                AtualPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            Board.putPiece(new King(Board, Color.White), new ChessPosition('d', 1).toPosition());
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).toPosition());
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).toPosition());
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).toPosition());
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).toPosition());
            Board.putPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).toPosition());

            Board.putPiece(new King(Board, Color.Black), new ChessPosition('d', 8).toPosition());
            Board.putPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).toPosition());
            Board.putPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).toPosition());
            Board.putPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).toPosition());
            Board.putPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).toPosition());
            Board.putPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).toPosition());
        }
    }
}
