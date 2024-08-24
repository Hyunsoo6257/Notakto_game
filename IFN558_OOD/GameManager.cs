using System; // using 지시문은 여기에 위치해야 함

namespace IFN558_OOD
{
    public class GameManager
    {
        private Game? currentGame; // Nullable reference type으로 변경

        public GameManager()
        {
            currentGame = null; // null 할당 허용
        }

        public void NewGame(string mode)
        {
            Console.WriteLine("Starting a new game...");

            if (mode.Equals("Notakto", StringComparison.OrdinalIgnoreCase))
            {
                currentGame = new Notakto();
            }
            else if (mode.Equals("Gomoku", StringComparison.OrdinalIgnoreCase))
            {
                currentGame = new Gomoku();
            }
            else
            {
                Console.WriteLine("Unknown game mode. Please choose either 'Notakto' of 'Gomoku'.");
                return;
            }

            currentGame.InitializeGame();
            currentGame.Start();
        }

        public void LoadGame(Game? game) // game 매개변수도 nullable로 변경
        {
            if (game == null)
            {
                Console.WriteLine("No saved game to load");
                return;
            }

            currentGame = game;
            Console.WriteLine("Game loaded successfully.");
            currentGame.Start();
        }

        public void Exit()
        {
            Console.WriteLine("Exiting the game. Thank you for playing!");
            Environment.Exit(0);
        }

        public void Help()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("new [game mode] - Start a new game of the specified mode (Notakto or Gomoku).");
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
                        if (commands.Length > 1)
                        {
                            NewGame(commands[1]);
                        }
                        else
                        {
                            Console.WriteLine("Please specify the game mode (Notakto or Gomoku).");
                        }
                        break;


                    case "load":
                        LoadGame(currentGame);
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
