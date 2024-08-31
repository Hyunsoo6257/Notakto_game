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
        public bool IsFirstPlayerTurn { get; set; }
        public IPlayer? Player1 { get; set; }
        public IPlayer? Player2 { get; set; }
        public Move GameMove { get; set; } = new Move(); // Manages move history for undo/redo
        public History GameHistory { get; set; } = new History(); // Manages saving/loading game state

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


        // Saves the current game state to history before exit
        public void SaveGameState()
        {
            GameHistory.SaveState((string[])Boards.Clone(), IsFirstPlayerTurn);
            Console.WriteLine("Game state saved.");
        }

        // Loads the most recent saved state from history
        public void LoadGameState(string[] savedState, bool isFirstPlayerTurn)
        {
            if (savedState != null && savedState.Length == Boards.Length)
            {
                Boards = savedState;
                IsFirstPlayerTurn = isFirstPlayerTurn; // Directly set turn from the parameter
                PrintBoard();
            }
            else
            {
                Console.WriteLine("Loaded state is invalid or does not match the current game setup.");
            }
        }

        protected string GetStoneMark(bool isFirstPlayerTurn)
        {
            return isFirstPlayerTurn ? "X" : "O";
        }
        // method to get the current state
        public string[] GetCurrentState()
        {
            return (string[])Boards.Clone(); // Return a copy of the current board state
        }

        // Method to handle undo/redo operations
        public void HandleUndoRedo(bool isUndo)
        {
            if (isUndo && GameMove.HasUndo())
            {
                int? lastMove = GameMove.Undo();

                if (lastMove.HasValue)
                {
                    // Undo the move: remove the stone from the board
                    Boards[lastMove.Value] = " ";
                    PrintBoard();

                    // Revert the player turn to the previous state
                }
                else
                {
                    Console.WriteLine("No moves to undo.");
                }
            }
            else if (!isUndo && GameMove.HasRedo())
            {
                int? nextMove = GameMove.Redo();

                if (nextMove.HasValue)
                {
                    // Redo the move: place the stone back on the board
                    Boards[nextMove.Value] = GetStoneMark(IsFirstPlayerTurn);
                    PrintBoard();

                    // Advance the player turn
                }
                else
                {
                    Console.WriteLine("No moves to redo.");
                }
            }
            else
            {
                Console.WriteLine("No more actions to undo/redo.");
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

