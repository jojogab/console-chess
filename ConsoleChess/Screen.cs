using ConsoleChess.board;
using ConsoleChess.board.Enums;
using ConsoleChess.chess;

namespace ConsoleChess
{
    internal class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if(board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");

            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece pie)
        {
            if(pie.Color == Color.White)
            {
                Console.Write(pie);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(pie);
                Console.ForegroundColor = aux;
            }
        }
    }
}
