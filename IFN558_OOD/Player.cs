using System;
namespace IFN558_OOD
{
    public interface IPlayer
    {
        string Name { get; set; }
        bool IsFirstPlayer { get; set; }

        void PlaceStone(string[] board, int boardIndex, int index, string stone);
    }

    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }
        public bool IsFirstPlayer { get; set; }

        public HumanPlayer(string name, bool isFirstPlayer)
        {
            Name = name;
            IsFirstPlayer = isFirstPlayer;
        }

        public void PlaceStone(string[] board, int boardIndex, int index, string stone)
        {
            // Calculate the correct index in the 1-dimensional array using boardIndex and cellIndex
            int adjustedIndex = boardIndex * (int)Math.Sqrt(board.Length / 3) * (int)Math.Sqrt(board.Length / 3) + index;
            if (board[adjustedIndex] == " ")
            {
                board[adjustedIndex] = stone;
            }
            else
            {
                Console.WriteLine("Invalid Move. The position is already occupied.");
            }
        }
    }

    public class ComputerPlayer : IPlayer
    {
        public string Name { get; set; }
        public bool IsFirstPlayer { get; set; }

        public ComputerPlayer(string name, bool isFirstPlayer)
        {
            Name = name;
            IsFirstPlayer = isFirstPlayer;
        }

        public void PlaceStone(string[] board, int boardIndex, int index, string stone)
        {
            Console.WriteLine("Computer is thinking...");

            Random random = new Random();
            bool emptySpotFound = false;
            int boardSize = (int)Math.Sqrt(board.Length / 3); // Adjust for multiple boards

            while (!emptySpotFound)
            {
                int randomRow = random.Next(0, boardSize);
                int randomCol = random.Next(0, boardSize);
                int adjustedIndex = boardIndex * boardSize * boardSize + randomRow * boardSize + randomCol;

                if (board[adjustedIndex] == " ")
                {
                    board[adjustedIndex] = stone;
                    emptySpotFound = true;
                }
            }
        }
    }


}

