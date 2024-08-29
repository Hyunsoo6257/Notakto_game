using System;
using System.Collections.Generic;

namespace IFN558_OOD
{
    public class History
    {
        private List<string[]> allStates = new List<string[]>();// To store all board states for resuming a game
        private List<bool> playerTurns = new List<bool>();

        // Saves the current board state into the list of all states
        public void SaveState(string[] currentState, bool isFirstPlayerTurn)
        {
            allStates.Add((string[])currentState.Clone());
            playerTurns.Add(isFirstPlayerTurn); // Save the player's turn
        }

        // Loads a saved state from a specific index
        public string[] LoadState(int index)
        {
            if (index >= 0 && index < allStates.Count)
            {
                return (string[])allStates[index].Clone();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid state index.");
            }
        }

        // Load the player's turn for a specific state
        public bool LoadPlayerTurn(int index)
        {
            return playerTurns[index]; // Return the player's turn at the specified index
        }

        // Returns the total number of saved states
        public int GetStateCount()
        {
            return allStates.Count;
        }

        // Clears all saved states
        public void ClearHistory()
        {
            allStates.Clear();
        }
    }
}
