using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IFN558_OOD

{
    public enum GameMode
    {
        HumanVsHuman,
        HumanVsComputer,
        ComputerVsHuman
    }

    public abstract class Game
    {
        public GameMode Mode { get; set; }
        public string[] Boards { get; set; }
        public Stack<string[]> UndoStack { get; set; } = new Stack<string[]>();
        public Stack<string[]> RedoStack { get; set; } = new Stack<string[]>();
        public bool IsFirstPlayerTurn { get; set; }
        public IPlayer? Player1 { get; set; }
        public IPlayer? Player2 { get; set; }

        public Game(bool isNewGame = true, GameMode? mode = null)
        {
            IsFirstPlayerTurn = true;

            // Initialize game mode and players
            if (isNewGame)
            {
                Console.WriteLine("1. Human vs Human");
                Console.WriteLine("2. Human vs Computer");
                Console.WriteLine("3. Computer vs Human");
                Console.Write("Select Mode:");

                string? input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        Mode = GameMode.HumanVsHuman;
                        Player1 = new HumanPlayer("Player 1", true);
                        Player2 = new HumanPlayer("Player 2", false);
                        break;

                    case "2":
                        Mode = GameMode.HumanVsComputer;
                        Player1 = new HumanPlayer("Player", true);
                        Player2 = new ComputerPlayer("Computer", false);
                        break;

                    case "3":
                        Mode = GameMode.ComputerVsHuman;
                        Player1 = new ComputerPlayer("Computer", true);
                        Player2 = new HumanPlayer("Player", false);
                        break;
                    default:
                        Console.WriteLine("Invalid Input!!! Please retry.");
                        break;

                }
            }
            else if (mode != null)
            {
                Mode = mode.Value;
            }
            else
            {
                throw new ArgumentException("Invalid Arguments for Game initialization. ");

            }
        }

        public abstract void InitializeGame();
        public abstract void Start();
        public abstract void PrintBoard();
        public abstract bool IsVaildPlace(int boardIndex, int cellIndex);
        public abstract bool IsGameOver();
        public abstract string GetWinner();

  
        public void Undo()
        {
            if (UndoStack.Count > 0)
            {
                RedoStack.Push((string[])UndoStack.Peek().Clone());
                UndoStack.Pop();
                IsFirstPlayerTurn = !IsFirstPlayerTurn;
            }

            else
            {
                Console.WriteLine("Undo is not available.");
            }
        }

        public void Redo()
        {
            if (RedoStack.Count > 0)
            {
                UndoStack.Push((string[])RedoStack.Peek().Clone());
                RedoStack.Pop();
                IsFirstPlayerTurn = !IsFirstPlayerTurn;
            }
            else
            {
                Console.WriteLine("Redo is not available.");
            }
        }

        public string GetPlayerName(bool isFirstPlayerTurn)
        {
            if (Mode == GameMode.HumanVsHuman)
            {
                return isFirstPlayerTurn ? "Player 1" : "Player 2";

            }
            else if (Mode == GameMode.HumanVsComputer)
            {
                return isFirstPlayerTurn ? "Player" : "Computer";
            }

            else
            {
                return isFirstPlayerTurn ? "Computer" : "Player";
            }
        }
    }


}

