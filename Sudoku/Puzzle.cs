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

        /*
        A board is solved when every row, box and column contains distinct numbers
        If the sum of the numbers in a row, a col or a box not equal 45, we don't need to check for distinct values in it
        */

        private List<List<int>> data { get; }
        private const int SIZE = 9;
        private const int BOXSIZE = 3;
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

        // returns the starting row of the box containing the given row
        private int StartRow(int row)
        {
            return row - (row % 3);
        }

        // returns the starting column of the box containing the given column
        private int StartCol(int col)
        {
            return col - (col % 3);
        }

        // check the sum of the given row
        private bool IsRowSumOk(int row)
        {
            int sum = 0;

            for(int col = 0; col < SIZE; col++)
            {
                sum += data[row][col];
            }

            return sum == 45;
        }

        // check the sum of the given column
        private bool IsColSumOk(int col)
        {
            int sum = 0;

            for(int row = 0; row < SIZE; row++)
            {
                sum += data[row][col];
            }

            return sum == 45;
        }

        // check the sum of the given box
        private bool IsBoxSumOk(int row, int col)
        {
            int startRow = StartRow(row);
            int startCol = StartCol(col);
            int sum = 0;

            for(int rowIdx = 0; rowIdx < BOXSIZE; rowIdx++)
            {
                for(int colIdx = 0; colIdx < BOXSIZE; colIdx++)
                {
                    sum += data[startRow + rowIdx][startCol + colIdx];
                }
            }

            return sum == 45;
        }

        // checks row, column and box sums from the given row and column
        private bool CheckSums(int row, int col)
        {
            return IsRowSumOk(row) && IsColSumOk(col) && IsBoxSumOk(row, col);
        }
    }
}
