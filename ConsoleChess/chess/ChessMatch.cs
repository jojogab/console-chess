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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            AtualPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            putPieces();
        }

        public void executemove(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.increasemoves();
            Piece capturedpiece =  Board.removePiece(destiny);
            Board.putPiece(p, destiny);
            if (capturedpiece != null)
            {
                Captured.Add(capturedpiece);
            }
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

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Captured)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> inGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void putNewPiece(char column, int line, Piece piece)
        {
            Board.putPiece(piece, new ChessPosition(column, line).toPosition());
            Pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('d', 1, new King(Board, Color.White));
            putNewPiece('c', 1, new Tower(Board, Color.White));
            putNewPiece('e', 1, new Tower(Board, Color.White));
            putNewPiece('c', 2, new Tower(Board, Color.White));
            putNewPiece('e', 2, new Tower(Board, Color.White));
            putNewPiece('d', 2, new Tower(Board, Color.White));

            putNewPiece('d', 8, new King(Board, Color.Black));
            putNewPiece('c', 8, new Tower(Board, Color.Black));
            putNewPiece('e', 8, new Tower(Board, Color.Black));
            putNewPiece('c', 7, new Tower(Board, Color.Black));
            putNewPiece('e', 7, new Tower(Board, Color.Black));
            putNewPiece('d', 7, new Tower(Board, Color.Black));
        }
    }
}
