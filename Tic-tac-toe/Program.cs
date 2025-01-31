using System.Runtime.InteropServices;

namespace TicTacToe
{
    class Program
    {
        static char currentPlayer = 'X';

        static void Main(string[] args)
        {
            ShowMenu();
        }

        static public void ShowMenu()
        {
            const string PlayCommand = "1";
            const string ExitCommand = "2";

            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();
                Messages.DisplayWelcomeMessage();

                Console.WriteLine("1 - PLAY \n" + "2 - EXIT \n");

                Console.Write("COMMAND: ");
                string userInput = Console.ReadLine();

                if (userInput == PlayCommand)
                {
                    Console.Clear();
                    StartGame();

                    break;
                }
                else if (userInput == ExitCommand)
                {
                    Console.Clear();
                    Console.Write("EXIT...");
                    Thread.Sleep(1000);

                    isWorking = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("NOT CORRECT!");
                    Console.ResetColor();
                    Thread.Sleep(800);

                    Console.Clear();
                }
            }
        }

        static void StartGame()
        {
            Dictionary<char, ConsoleColor> playerColors = new Dictionary<char, ConsoleColor>()
            {
                { 'X', ConsoleColor.Magenta }, 
                { 'O', ConsoleColor.Blue }
            };

            Board board = new Board(playerColors);

            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                board.DrawBoard();

                var choice = PlayerInput(board, playerColors[currentPlayer]);
                
                board.PlaceMarker(choice, currentPlayer);
                
                if (board.CheckWin(currentPlayer))
                {
                    Console.Clear();
                    board.DrawBoard();

                    Messages.DisplayWinMessage(currentPlayer);

                    break;
                }

                if (board.CheckDraw())
                {
                    Console.Clear();
                    board.DrawBoard();

                    Messages.DisplayDrawMessage();

                    break;
                }

                SwichPlayer();
            }
        }

        // public static void SetPlayerColor(char currentPlayer)
        // {
        //     if (currentPlayer == 'X')
        //         Console.ForegroundColor = ConsoleColor.Magenta;
        //     else if (currentPlayer == 'O')
        //         Console.ForegroundColor = ConsoleColor.Blue;
        //     else
        //         Console.ResetColor();
        // }

        public static int PlayerInput(Board board, ConsoleColor color)
        {
            int choice;

            Console.ForegroundColor = color;
            Console.Write($"PLAYER {currentPlayer}. YOUR CHOICE (1-{board.Length}): ");
            Console.ResetColor();

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > board.Length || !board.IsCellAvailable(choice))
            {
                Console.WriteLine("NOT CORRECT!");
            }

            return choice;
        }

        public static void SwichPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}
