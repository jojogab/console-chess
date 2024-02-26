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
        try
        {
            Console.Clear();
            Screen.printBoard(match.Board);
            Console.WriteLine();
            Console.WriteLine($"Turn: {match.Turn}");
            Console.WriteLine($"Waiting move: {match.AtualPlayer}");

            Console.WriteLine();
            Console.Write("Origin: ");
            Position origin = Screen.readChessPosition().toPosition();
            match.validateOriginPosition(origin);

            bool[,] possibleMoves = match.Board.piece(origin).possibleMoves();

            Console.Clear();
            Screen.printBoard(match.Board, possibleMoves);

            Console.Write("Destiny: ");
            Position destiny = Screen.readChessPosition().toPosition();
            match.validateDestinyPosition(origin, destiny);

            match.makeMove(origin, destiny);
        }
        catch(BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }  
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}
