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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            AtualPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece executemove(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.increasemoves();
            Piece capturedpiece =  Board.removePiece(destiny);
            Board.putPiece(p, destiny);
            if (capturedpiece != null)
            {
                Captured.Add(capturedpiece);
            }

            //#SpecialMove Little Roque
            if(p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.removePiece(originT);
                T.increasemoves();
                Board.putPiece(T, destinyT);
            }

            //#SpecialMove Big Roque
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.removePiece(originT);
                T.increasemoves();
                Board.putPiece(T, destinyT);
            }

            //#SpecialMove En Passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedpiece == null)
                {
                    Position posP;
                    if(p.Color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    capturedpiece = Board.removePiece(posP);
                    Captured.Add(capturedpiece);
                }
            }

            return capturedpiece;
        }

        public void undoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.removePiece(destiny);
            p.decreasemoves();
            if(capturedPiece != null)
            {
                Board.putPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.putPiece(p, origin);

            //#SpecialMove Little Roque
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.removePiece(destinyT);
                T.decreasemoves();
                Board.putPiece(T, originT);
            }

            //#SpecialMove Big Roque
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.removePiece(destinyT);
                T.decreasemoves();
                Board.putPiece(T, originT);
            }

            //#SpecialMove En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.removePiece(destiny);

                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    Board.putPiece(pawn, posP);
                }

            }
        }

        public void makeMove(Position origin, Position destiny)
        {
            Piece capturedpiece = executemove(origin, destiny);

            if (isInCheck(AtualPlayer))
            {
                undoMove(origin, destiny, capturedpiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece p = Board.piece(destiny);

            //#SpecialMove Promotion
            if(p is Pawn)
            {
                if(p.Color == Color.White && destiny.Line == 0 || p.Color == Color.Black && destiny.Line == 7)
                {
                    p = Board.removePiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.putPiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }

            if (isInCheck(opponent(AtualPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (testCheckMate(opponent(AtualPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                changePlayer();
            }

            //#SpecialMove En Passant
            if(p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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

        private Color opponent(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach(Piece x in inGamePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece k = king(color);
            if (k == null)
            {
                throw new BoardException($"There is no {color} king on the board!");
            }

            foreach(Piece x in inGamePieces(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach(Piece x in inGamePieces(color))
            {
                bool[,] mat = x.possibleMoves();
                for(int i = 0; i < Board.Lines; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = executemove(origin, destiny);
                            bool testCheck = isInCheck(color);
                            undoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int line, Piece piece)
        {
            Board.putPiece(piece, new ChessPosition(column, line).toPosition());
            Pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Tower(Board, Color.White));
            putNewPiece('b', 1, new Horse(Board, Color.White));
            putNewPiece('c', 1, new Bishop(Board, Color.White));
            putNewPiece('d', 1, new Queen(Board, Color.White));
            putNewPiece('e', 1, new King(Board, Color.White, this));
            putNewPiece('f', 1, new Bishop(Board, Color.White));
            putNewPiece('g', 1, new Horse(Board, Color.White));
            putNewPiece('h', 1, new Tower(Board, Color.White));
            putNewPiece('a', 2, new Pawn(Board, Color.White, this));
            putNewPiece('b', 2, new Pawn(Board, Color.White, this));
            putNewPiece('c', 2, new Pawn(Board, Color.White, this));
            putNewPiece('d', 2, new Pawn(Board, Color.White, this));
            putNewPiece('e', 2, new Pawn(Board, Color.White, this));
            putNewPiece('f', 2, new Pawn(Board, Color.White, this));
            putNewPiece('g', 2, new Pawn(Board, Color.White, this));
            putNewPiece('h', 2, new Pawn(Board, Color.White, this));

            putNewPiece('a', 8, new Tower(Board, Color.Black));
            putNewPiece('b', 8, new Horse(Board, Color.Black));
            putNewPiece('c', 8, new Bishop(Board, Color.Black));
            putNewPiece('d', 8, new Queen(Board, Color.Black));
            putNewPiece('e', 8, new King(Board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(Board, Color.Black));
            putNewPiece('g', 8, new Horse(Board, Color.Black));
            putNewPiece('h', 8, new Tower(Board, Color.Black));
            putNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
