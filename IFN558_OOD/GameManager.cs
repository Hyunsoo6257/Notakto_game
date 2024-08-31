using System; // using 지시문은 여기에 위치해야 함

namespace IFN558_OOD
{
    public class GameManager
    {
        private Game? currentGame; // Nullable reference type으로 변경
        private History gameHistory = new History();

        public GameManager()
        {
            currentGame = null; // null 할당 허용
        }

        public void NewGame()
        {
            Console.WriteLine("Starting a new game...");
            Console.WriteLine("Please choose a game mode:");
            Console.WriteLine("1. Notakto");
            Console.WriteLine("2. Gomoku");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    currentGame = new Notakto();
                    break;
                case "2":
                    currentGame = new Gomoku();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose either '1' for Notakto or '2' for Gomoku.");
                    return; // Exit the method if an invalid choice is made
            }

            currentGame.InitializeGame();
            currentGame.Start();
        }

        public void LoadGame()
        {
            if (gameHistory.GetStateCount() > 0)
            {
                Console.WriteLine("Loading the most recent saved game...");
                string[] lastState = gameHistory.LoadState(gameHistory.GetStateCount() - 1);
                bool isFirstPlayerTurn = gameHistory.LoadPlayerTurn(gameHistory.GetStateCount() - 1); // Assume this method exists
                currentGame.LoadGameState(lastState, isFirstPlayerTurn); // Pass both state and turn
                currentGame.Start();
            }
            else
            {
                Console.WriteLine("No saved game to load.");
            }
        }

        public void Exit()
        {
            if (currentGame != null)
            {
                Console.WriteLine("Saving current game state before exiting...");
                gameHistory.SaveState(currentGame.GetCurrentState(), currentGame.IsFirstPlayerTurn); // Save both state and turn
                Console.WriteLine("Exiting the game. Thank you for playing!");
                Environment.Exit(0);
            }

        }
        public void Help()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("new - Start a new game.");
            Console.WriteLine("load - Load the most recent saved game.");
            Console.WriteLine("exit - Exit the game.");
            Console.WriteLine("help - Show this help message.");
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Enter command new/load/exit/help: ");
                string input = Console.ReadLine().ToLower();
                string[] commands = input.Split(' ');

                switch (commands[0])
                {
                    case "new":
                        NewGame(); // Call the updated NewGame method without parameters
                        break;


                    case "load":
                        LoadGame();
                        break;

                    case "exit":
                        Exit();
                        isRunning = false;
                        break;

                    case "help":
                        Help();
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}
