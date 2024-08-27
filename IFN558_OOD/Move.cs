using System;
namespace IFN558_OOD
{
    public class Move
    {
        private Stack<int> moveStack = new Stack<int>(); // Stack to store the positions of the moves
        private Stack<int> redoStack = new Stack<int>(); // Stack to store moves for redo operation

        // Saves the position of the move.
        public void SaveMove(int position)
        {
            moveStack.Push(position);  // Push the new move onto the move stack
            redoStack.Clear(); // Clear the redo stack when a new move is made
        }

        // Undoes the last move.
        public int? Undo()
        {
            if (moveStack.Count > 0)
            {
                int lastMove = moveStack.Pop(); // Remove the last move from the move stack
                redoStack.Push(lastMove); // Save the undone move to the redo stack
                return lastMove; // Return the undone move position
            }
            return null; // Return null if no moves are available to undo
        }

        // Redoes the last undone move.
        public int? Redo()
        {
            if (redoStack.Count > 0)
            {
                int moveToRedo = redoStack.Pop(); // Remove the last move from the redo stack
                moveStack.Push(moveToRedo); // Save the redone move back to the move stack
                return moveToRedo; // Return the redone move position
            }
            return null; // Return null if no moves are available to redo
        }

        // Checks if an undo operation is possible.
        public bool HasUndo()
        {
            return moveStack.Count > 0; // Return true if there are moves to undo
        }

        // Checks if a redo operation is possible.
        public bool HasRedo()
        {
            return redoStack.Count > 0; // Return true if there are moves to redo
        }

        // Gets the last move made (that hasn't been undone).
        public int? GetLastMove()
        {
            return moveStack.Count > 0 ? (int?)moveStack.Peek() : null; // Return the last move without removing it
        }
    }

}

