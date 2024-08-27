using System;
using System.Collections.Generic;

namespace IFN558_OOD
{
    public class History
    {
        private List<string[]> allStates = new List<string[]>(); // List to store all board states

        // Saves the current board state.
        public void SaveState(string[] state)
        {
            allStates.Add((string[])state.Clone()); // Clone the board state and add it to the list
        }

        // Clears all stored states in the history.
        public void ClearHistory()
        {
            allStates.Clear(); // Clear the list of all states
        }
    }
}

