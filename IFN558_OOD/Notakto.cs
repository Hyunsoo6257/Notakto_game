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

        public override void Start()
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
                    Console.WriteLine("Enter your move (boardIndex cellIndex): ");
                    string input = Console.ReadLine();
                    string[] inputs = input.Split(' ');
                    int boardIndex = int.Parse(inputs[0]);
                    int cellIndex = int.Parse(inputs[1]);

                    if (IsVaildPlace(boardIndex, cellIndex))
                    {
                        currentPlayer.PlaceStone(Boards, boardIndex, cellIndex, GetStoneMark(IsFirstPlayerTurn));
                    }
                    else
                    {
                        Console.WriteLine("Invalid move, please try again.");
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

