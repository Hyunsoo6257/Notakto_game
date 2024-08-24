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
            
        }
    }


}

