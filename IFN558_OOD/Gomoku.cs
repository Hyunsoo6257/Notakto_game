using System;

namespace IFN558_OOD
{
    public class Gomoku : Game
    {
        public string[] Boards { get; set; }
        public int NumberOfBoards { get; set; }
        public int Size { get; set; }

        public Gomoku(bool isNewGame = true, GameMode? mode = null)
            : base(isNewGame, mode)
        {
            NumberOfBoards = 1; // Gomoku는 하나의 보드에서 플레이됨
            Size = 225; // 15x15 보드이므로 총 셀 개수는 225
            Boards = new string[NumberOfBoards * Size];
            InitializeGame();
        }

        public override void InitializeGame()
        {
            for (int i = 0; i < Boards.Length; i++)
            {
                Boards[i] = " "; // 보드의 모든 칸을 빈 칸으로 초기화
            }
            Console.WriteLine("Gomoku game initialized.");
        }

        public override void Start()
        {
            Console.WriteLine("Starting Gomoku game...");
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
                    Console.WriteLine("Enter your move (cellIndex): ");
                    string input = Console.ReadLine();
                    int cellIndex = int.Parse(input);

                    if (IsVaildPlace(0, cellIndex))
                    {
                        currentPlayer.PlaceStone(Boards, 0, cellIndex, GetStoneMark(IsFirstPlayerTurn));
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
            int boardSide = (int)Math.Sqrt(Size); // 보드의 한 변의 크기

            for (int i = 0; i < boardSide; i++)
            {
                for (int j = 0; j < boardSide; j++)
                {
                    int index = i * boardSide + j;
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
        }

        public override bool IsVaildPlace(int boardIndex, int cellIndex)
        {
            return Boards[cellIndex] == " "; // 빈 칸인지 확인
        }

        public override bool IsGameOver()
        {
            // Gomoku에서 게임 종료 조건을 확인하는 로직
            int boardSide = (int)Math.Sqrt(Size);

            // 수평, 수직, 대각선으로 5개의 동일한 돌이 있는지 확인
            for (int i = 0; i < boardSide; i++)
            {
                for (int j = 0; j < boardSide; j++)
                {
                    int index = i * boardSide + j;

                    if (Boards[index] == " ") continue; // 빈 칸이면 건너뛰기

                    // 수평 체크
                    if (j <= boardSide - 5 && CheckLine(index, 1)) return true;

                    // 수직 체크
                    if (i <= boardSide - 5 && CheckLine(index, boardSide)) return true;

                    // 대각선 체크
                    if (i <= boardSide - 5 && j <= boardSide - 5 && CheckLine(index, boardSide + 1)) return true;
                    if (i >= 4 && j <= boardSide - 5 && CheckLine(index, boardSide - 1)) return true;
                }
            }

            return false;
        }

        public override string GetWinner()
        {
            return IsFirstPlayerTurn ? "Player 2" : "Player 1";
        }

        private bool CheckLine(int startIndex, int step)
        {
            string firstStone = Boards[startIndex];

            for (int i = 1; i < 5; i++)
            {
                if (Boards[startIndex + step * i] != firstStone) return false;
            }

            return true;
        }

        private string GetStoneMark(bool isFirstPlayerTurn)
        {
            return isFirstPlayerTurn ? "X" : "O";
        }
    }
}
