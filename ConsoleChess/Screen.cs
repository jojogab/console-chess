using ConsoleChess.board;
using ConsoleChess.board.Enums;
using ConsoleChess.chess;

namespace ConsoleChess
{
    internal class Screen
    {
        public static void printMatch (ChessMatch match)
        {
            printBoard(match.Board);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Turn: {match.Turn}");
            Console.WriteLine($"Waiting move: {match.AtualPlayer}");
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            printSet(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {                  
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor ogBackgroung = Console.BackgroundColor;
            ConsoleColor changedBackgroung = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = changedBackgroung;
                    }
                    else
                    {
                        Console.BackgroundColor = ogBackgroung;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = ogBackgroung;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = ogBackgroung;
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
            if (pie == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (pie.Color == Color.White)
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
                Console.Write(" ");
            }
        }
    }
}
