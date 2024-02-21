using ConsoleChess;
using ConsoleChess.board;
using ConsoleChess.board.Enums;
using ConsoleChess.board.Exceptions;
using ConsoleChess.chess;

try
{
    Board board = new Board(8, 8);

    board.putPiece(new Tower(board, Color.Black), new Position(0, 0));
    board.putPiece(new Tower(board, Color.Black), new Position(1, 5));
    board.putPiece(new King(board, Color.Black), new Position(0, 2));

    board.putPiece(new Tower(board, Color.White), new Position(7, 0));
    board.putPiece(new Tower(board, Color.White), new Position(6, 5));
    board.putPiece(new King(board, Color.White), new Position(7, 2));

    Screen.printBoard(board);   
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
