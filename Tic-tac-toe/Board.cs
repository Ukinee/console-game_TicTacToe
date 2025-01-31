namespace TicTacToe
{
    public class Board
    {
        private readonly Dictionary<char, ConsoleColor> _playerColors;
        private const int Size = 4;
        private const char Empty = ' ';
        private char[,] _board;

        public Board(Dictionary<char, ConsoleColor> playerColors)
        {
            _playerColors = playerColors;
            _board = new char[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _board[i, j] = Empty;
                }
            }
        }
        
        public int Length => _board.Length;

        public void DrawBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Console.WriteLine($"     ║     ║     \n" +
            //                   $"  {board[0, 0]}  ║  {board[0, 1]}  ║  {board[0, 2]}  \n" +
            //                   $"     ║     ║     \n" +
            //                   $"═════║═════║═════\n" +
            //                   $"     ║     ║     \n" +
            //                   $"  {board[1, 0]}  ║  {board[1, 1]}  ║  {board[1, 2]}  \n" +
            //                   $"     ║     ║     \n" +
            //                   $"═════║═════║═════\n" +
            //                   $"     ║     ║     \n" +
            //                   $"  {board[2, 0]}  ║  {board[2, 1]}  ║  {board[2, 2]}  \n" +
            //                   $"     ║     ║     \n");

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    WriteSymbolOrNumber(i, j);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        private void WriteSymbolOrNumber(int rowIndex, int columnIndex)
        {
            char symbol = _board[rowIndex, columnIndex];

            if (symbol != Empty)
            {
                ConsoleColor currentColor = Console.ForegroundColor;
                Console.ForegroundColor = _playerColors[symbol];
                
                Console.Write($"{symbol, 3} ");
                
                Console.ForegroundColor = currentColor;
                return;
            }

            Console.Write($"{rowIndex * Size + columnIndex + 1, 3} ");
        }

        public bool IsCellAvailable(int choice)
        {
            int row = (choice - 1) / Size;
            int col = (choice - 1) % Size;

            return _board[row, col] == Empty;
        }

        public void PlaceMarker(int choice, char currentPlayer)
        {
            int row = (choice - 1) / Size;
            int col = (choice - 1) % Size;
            _board[row, col] = currentPlayer;
        }

        public bool CheckWin(char currentPlayer)
        {
            // for (int i = 0; i < 3; i++)
            // {
            //     if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
            //         return true;
            //
            //     if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
            //         return true;
            // }
            //
            // if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            //     return true;
            //
            // if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[0, 2] == currentPlayer)
            //     return true;

            for (int i = 0; i < Size; i++)
            {
                bool isFullRow = true;

                for (int j = 0; j < Size; j++)
                {
                    if (_board[i, j] == currentPlayer)
                        continue;

                    isFullRow = false;

                    break;
                }

                bool isFullColumn = true;

                for (int j = 0; j < Size; j++)
                {
                    if (_board[j, i] == currentPlayer)
                        continue;

                    isFullColumn = false;

                    break;
                }

                if (isFullRow || isFullColumn)
                    return true;
            }

            bool isFullDiagonal = true;

            for (int i = 0; i < Size; i++)
            {
                if (_board[i, i] != currentPlayer)
                {
                    isFullDiagonal = false;

                    break;
                }
            }

            if (isFullDiagonal)
                return true;

            isFullDiagonal = true;

            for (int i = 0; i < Size; i++)
            {
                if (_board[i, Size - 1 - i] != currentPlayer)
                {
                    isFullDiagonal = false;

                    break;
                }
            }

            if (isFullDiagonal)
                return true;

            return false;
        }

        public bool CheckDraw()
        {
            foreach (var cell in _board)
            {
                if (cell == Empty)
                    return false;
            }

            return true;
        }
    }
}
