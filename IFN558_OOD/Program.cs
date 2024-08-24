namespace IFN558_OOD;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "IFN558 OOD Game Manager";

        GameManager gameManager = new GameManager();

        Console.WriteLine("Welcome to the Game Manager!");
        Console.WriteLine("Type 'help' to see available commands.");

        gameManager.Run();
    }
}

