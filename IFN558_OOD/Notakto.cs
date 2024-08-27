using System;
namespace IFN558_OOD
{
	public class Notakto : Game
	{
		public string[] Boards { get; set; }
		public int NumberOfBoards { get; set; }
		public int Size { get; set; }

        public Notakto(bool isNewGame = true, GameMode? mode = null)
            : base(isNewGame, mode)
        {
            NumberOfBoards = 3; 
            Size = 9; 
            Boards = new string[NumberOfBoards * Size]; 
            InitializeGame();
        }

        public override void InitializeGame()
        {
            for (int i = 0; i < Boards.Length; i++)
            {
                Boards[i] = " ";
            }

            Console.WriteLine("Notakto game initialized.");

        }

        public override void PlayGame()
        {
            Console.WriteLine("Starting Notakto game...");
            while (true)
            {
                PrintBoard();
                if (IsGameOver())
                {
                    string winner = GetWinner();
                    Console.WriteLine($"{winner} is the winner!!!");
                    break;
                }

                IPlayer currentPlayer = IsFirstPlayerTurn ? Player1 : Player2;
                Console.WriteLine($"{currentPlayer.Name}'s turn:");

                if (currentPlayer is HumanPlayer)
                {
                    Console.WriteLine("Enter your move (boardIndex row column): ");
                    string input = Console.ReadLine();

                    // Allow both space and comma as delimiters
                    char[] separators = new char[] { ' ', ',' };
                    string[] inputs = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    // Check if the input is given in three parts
                    if (inputs.Length != 3)
                    {    
                        Console.WriteLine("Invalid input format. Please enter three numbers separated by a space or comma.");
                        continue;
                    }

                    // Parse input into integers and validate
                    if (!int.TryParse(inputs[0], out int boardIndex) ||
                        !int.TryParse(inputs[1], out int row) ||
                        !int.TryParse(inputs[2], out int column))
                    {
                        Console.WriteLine("Invalid input. Please enter valid numbers for boardIndex, row, and column.");
                        continue;
                    }

                    // Adjust inputs to be 0-based index
                    boardIndex -= 1; // Adjust boardIndex to 0-based
                    row -= 1;        // Adjust row to 0-based
                    column -= 1;     // Adjust column to 0-based

                    int cellIndex = row * (int)Math.Sqrt(Size) + column;

                    // Check if the input indices are within valid range
                    if (boardIndex < 0 || boardIndex >= NumberOfBoards ||
                        row < 0 || row >= Math.Sqrt(Size) ||
                        column < 0 || column >= Math.Sqrt(Size))
                    {
                        Console.WriteLine("Invalid move, index out of range. Please try again.");
                        continue;
                    }


                    // Check if the cell is valid and empty
                    if (IsVaildPlace(boardIndex, cellIndex))
                    {
                        currentPlayer.PlaceStone(Boards, boardIndex, cellIndex, GetStoneMark(IsFirstPlayerTurn));
                    }
                    else
                    {
                        Console.WriteLine("Invalid move, the cell is already occupied. Please try again.");
                        continue;
                    }
                }

                else if (currentPlayer is ComputerPlayer)
                {
                    currentPlayer.PlaceStone(Boards, 0, 0, GetStoneMark(IsFirstPlayerTurn));
                }

                IsFirstPlayerTurn = !IsFirstPlayerTurn;
            }
        }

        public override void PrintBoard()
        {
            int boardSide = (int)Math.Sqrt(Size); 
            
            for (int b = 0; b < NumberOfBoards; b++)
            {
                Console.WriteLine($"Board {b + 1}:");
                for (int i = 0; i < boardSide; i++)
                {
                    for (int j = 0; j < boardSide; j++)
                    {
                        int index = b * Size + i * boardSide + j;
                        Console.Write($" {Boards[index]} ");
                        if (j < boardSide - 1) Console.Write("|");
                    }
                    Console.WriteLine();

                    if (i < boardSide - 1)
                    {
                        for (int j = 0; j < boardSide; j++)
                        {
                            Console.Write("---");
                            if (j < boardSide - 1) Console.Write("+");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
        }
        public override bool IsVaildPlace(int boardIndex, int cellIndex)
        {
            int index = boardIndex * Size + cellIndex;
            return Boards[index] == " ";
        }

        public override bool IsGameOver()
        {
            
            foreach (var cell in Boards)
            {
                if (cell == " ") return false;
            }
            return true;
        }

        public override string GetWinner()
        {
            return IsFirstPlayerTurn ? "Player 2" : "Player 1";
        }

        private string GetStoneMark(bool isFirstPlayerTurn)
        {
            return isFirstPlayerTurn ? "X" : "O";
        }
    }
}

