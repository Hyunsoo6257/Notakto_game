﻿using System;
namespace IFN558_OOD
{
	public class Notakto : Game
	{
		
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
                PrintBoard(); // Display the current board

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
                    bool moveConfirmed = false; // Whether the player has confirmed the move

                    while (!moveConfirmed)
                    {
                        Console.WriteLine("Enter your move (boardIndex row column): ");
                        string input = Console.ReadLine();

                        char[] separators = new char[] { ' ', ',' };
                        string[] inputs = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        if (inputs.Length != 3)
                        {
                            Console.WriteLine("Invalid input format. Please enter three numbers separated by a space or comma.");
                            continue;
                        }

                        if (!int.TryParse(inputs[0], out int boardIndex) ||
                            !int.TryParse(inputs[1], out int row) ||
                            !int.TryParse(inputs[2], out int column))
                        {
                            Console.WriteLine("Invalid input. Please enter valid numbers for boardIndex, row, and column.");
                            continue;
                        }

                        boardIndex -= 1; // Convert to 0-based index
                        row -= 1;
                        column -= 1;

                        int cellIndex = row * (int)Math.Sqrt(Size) + column;

                        if (boardIndex < 0 || boardIndex >= NumberOfBoards ||
                            row < 0 || row >= Math.Sqrt(Size) ||
                            column < 0 || column >= Math.Sqrt(Size))
                        {
                            Console.WriteLine("Invalid move, index out of range. Please try again.");
                            continue;
                        }

                        if (IsVaildPlace(boardIndex, cellIndex))
                        {
                            // Save the move before applying it
                            GameMove.SaveMove(boardIndex * Size + cellIndex);
                            currentPlayer.PlaceStone(Boards, boardIndex, cellIndex, GetStoneMark(IsFirstPlayerTurn));
                            PrintBoard(); // Display the current board state

                            // Ask the player if they want to keep the move or undo
                            bool validChoice = false;
                            while (!validChoice)
                            {
                                Console.WriteLine("Do you want to keep this move or undo? (k to keep, u to undo): ");
                                string choice = Console.ReadLine()?.ToLower();

                                if (choice == "u")
                                {
                                    HandleUndoRedo(true); // Execute undo logic
                                    validChoice = true;  // Set valid choice to true
                                    moveConfirmed = false; // Reset move confirmation
                                }
                                else if (choice == "k")
                                {
                                    moveConfirmed = true; // Confirm the move
                                    validChoice = true;   // Set valid choice to true
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice. Please enter 'k' to keep or 'u' to undo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid move, the cell is already occupied. Please try again.");
                        }
                    }

                    // Only toggle turn after a move is confirmed to be kept
                    if (moveConfirmed)
                    {
                        IsFirstPlayerTurn = !IsFirstPlayerTurn;
                    }
                }
                else if (currentPlayer is ComputerPlayer)
                {
                    currentPlayer.PlaceStone(Boards, 0, 0, GetStoneMark(IsFirstPlayerTurn));
                    PrintBoard(); // Display the current board state

                    // Toggle turn after the computer's move
                    IsFirstPlayerTurn = !IsFirstPlayerTurn;
                }
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


    }
}

