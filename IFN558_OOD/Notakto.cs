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
            
        }

        public override void Start()
        {
           
        }

        public override void PrintBoard()
        {
           
        }
    }
}

