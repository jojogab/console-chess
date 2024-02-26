using ConsoleChess;
using ConsoleChess.board;
using ConsoleChess.board.Enums;
using ConsoleChess.board.Exceptions;
using ConsoleChess.chess;

try
{
    ChessMatch match = new ChessMatch();

    while (!match.Finished)
    {
        Console.Clear();
        Screen.printBoard(match.Board);

        Console.WriteLine();
        Console.Write("Origem: ");
        Position origin = Screen.readChessPosition().toPosition();

        bool[,] possibleMoves = match.Board.piece(origin).possibleMoves();

        Console.Clear();
        Screen.printBoard(match.Board, possibleMoves);

        Console.Write("Destino: ");
        Position destiny = Screen.readChessPosition().toPosition();

        match.executemove(origin, destiny);
    }  
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
