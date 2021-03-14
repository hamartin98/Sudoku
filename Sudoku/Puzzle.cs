using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    // represents a sudoku board and all functionality wich can be made on it
    class Puzzle
    {
        // functions needed to be implemented
        // check row, column, box sums
        // check row, column, box duplicates
        // find solution
        // check if a row, col or a box is safe

        private List<List<int>> data { get; }
        private int size = 9;
        public Puzzle(List<List<int>> data)
        {
            this.data = data;
        }

        // returns a value from the cell based on row and col
        public int ValueAt(int row, int col)
        {
            return data[row][col];
        }

        // sets the value at the specified row and col
        public void SetValue(int value, int row, int col)
        {
            data[row][col] = value;
        }
    }
}
