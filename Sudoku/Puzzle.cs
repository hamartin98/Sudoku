using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sudoku
{
    // represents a sudoku board and all functionality wich can be made on it
    public class Puzzle
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

        private List<List<int>> data;
        private const int SIZE = 9;
        private const int BOXSIZE = 3;
        private int zeroCount = 0;

        public delegate void PuzzleSolvedEventHandler(object sender, EventArgs args);
        public event PuzzleSolvedEventHandler PuzzleSolved;

        public Puzzle(List<List<int>> data)
        {
            this.data = data;
            zeroCount = countZeroes();
        }

        public void SetPuzzle(List<List<int>> data)
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
            zeroCount += value == 0 ? 1 : -1; // increase or decrease the count of the zeroes
            zeroCount = Math.Min(0, zeroCount);
            IsSolved();
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

        // check duplicated values in the given row expect 0 values
        private bool CheckRowDuplicates(int row)
        {
            int[] duplicates = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for(int col = 0; col < SIZE; col++)
            {
                int num = data[row][col];
                if (num != 0 && duplicates[num - 1] < 1)
                {
                    duplicates[num - 1]++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        // check duplicated values in the given row expect 0 values
        private bool CheckColDuplicates(int col)
        {
            int[] duplicates = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int row = 0; col < SIZE; col++)
            {
                int num = data[row][col];
                if (num != 0 && duplicates[num - 1] < 1)
                {
                    duplicates[num - 1]++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        // check duplicates in the given box except 0 values
        private bool CheckBoxDuplicates(int row, int col)
        {
            int[] duplicates = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int startRow = StartRow(row);
            int startCol = StartCol(col);

            for (int rowIdx = 0; rowIdx < BOXSIZE; rowIdx++)
            {
                for (int colIdx = 0; colIdx < BOXSIZE; colIdx++)
                {
                    int num = data[row + rowIdx][col + colIdx];
                    if (num != 0 && duplicates[num - 1] < 1)
                    {
                        duplicates[num - 1]++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // check the sum of all rows
        // returns true if every row contains the correct sum
        private bool checkAllRowSum()
        {
            for(int row = 0; row < SIZE; row++)
            {
                if(!IsRowSumOk(row))
                {
                    return false;
                }
            }

            return true;
        }

        // check the sum of all columns
        // returns true if every column contains the correct sum
        private bool checkAllColSum()
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (!IsRowSumOk(col))
                {
                    return false;
                }
            }

            return true;
        }

        // check the sum of all boxes
        // returns true if every box contains the correct sum
        private bool checkAllBoxSum()
        {
            for(int boxRow = 0; boxRow < SIZE; boxRow += 3)
            {
                for(int boxCol = 0; boxCol < SIZE; boxCol += 3)
                {
                    if(!IsBoxSumOk(boxRow, boxCol))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // check all rows for duplicates
        // returns true if every row contains distinct values
        private bool CheckAllRowDuplicate()
        {
            for(int row = 0; row < SIZE; row++)
            {
                if(!CheckRowDuplicates(row))
                {
                    return false;
                }
            }

            return true;
        }

        // check all columns for duplicates
        // returns true if every column contains distinct values
        private bool CheckAllColDuplicate()
        {
            for (int col = 0; col < SIZE; col++)
            {
                if (!CheckColDuplicates(col))
                {
                    return false;
                }
            }

            return true;
        }

        // check all boxes for duplicates
        // returns true if every box contains distinct values
        private bool CheckAllBoxDuplicate()
        {
            for (int boxRow = 0; boxRow < SIZE; boxRow += 3)
            {
                for (int boxCol = 0; boxCol < SIZE; boxCol += 3)
                {
                    if (!CheckBoxDuplicates(boxRow, boxCol))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // returns true if the whole board is solved
        private bool IsSolved()
        {
            if(checkAllRowSum() && checkAllColSum() && checkAllBoxSum())
            {
                if(CheckAllRowDuplicate() && CheckAllColDuplicate() && CheckAllBoxDuplicate())
                {
                    OnPuzzleSolved();
                    return true;
                }
            }

            return false;
        }
        
        // count all zeroes on the board
        private int countZeroes()
        {
            int count = 0;

            foreach(var row in data)
            {
                foreach(var value in row)
                {
                    if(value == 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        // Triggered when the puzzle is solved
        protected virtual void OnPuzzleSolved()
        {
            if (PuzzleSolved != null)
            {
                PuzzleSolved(this, EventArgs.Empty);
            }
        }

        // Finds an empty location on the board, returns true if found one
        private bool FindEmptyLocation(out int resRow, out int resCol)
        {
            resRow = -1;
            resCol = -1;

            for (int row = 0; row < SIZE; row++)
            {
                for(int col = 0; col < SIZE; col++)
                {
                    if(data[row][col] == 0)
                    {
                        resRow = row;
                        resCol = col;
                        return true;
                    }
                }
            }

            return false;
        }

        // Returns true if the row contains the number
        private bool IsRowContains(int row, int number)
        {
            for(int col = 0; col < SIZE; col++)
            {
                if(ValueEqualsAt(number, row, col))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns true if the given column contains the number
        private bool IsColContains(int col, int number)
        {
            for(int row = 0; row < SIZE; row++)
            {
                if(ValueEqualsAt(number, row, col))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns true if the given box contains the number
        private bool IsBoxContains(int row, int col, int number)
        {
            int startRow = StartRow(row);
            int startCol = StartCol(col);

            for(int rowIdx = 0; rowIdx < BOXSIZE; rowIdx++)
            {
                for(int colIdx = 0; colIdx < BOXSIZE; colIdx++)
                {
                    if(ValueEqualsAt(number, startRow + rowIdx, startCol + colIdx))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Returns true if the value of the given position equals with the given value
        private bool ValueEqualsAt(int value, int row, int col)
        {
            return data[row][col] == value;
        }

        // Returns true if the given value is not in the specified row, column and box
        private bool AllSafe(int row, int col, int number)
        {
            return !IsRowContains(row, number) && !IsColContains(col, number) && !IsBoxContains(row, col, number);
        }

        // Finds the solution to the data
        private bool FindSolution()
        {
            int row;
            int col;
            if (!FindEmptyLocation(out row, out col))
            {
                return true;
            }

            for (int number = 1; number <= SIZE; number++)
            {
                if (AllSafe(row, col, number))
                {
                    data[row][col] = number;

                    if (FindSolution())
                    {
                        return true;
                    }

                    data[row][col] = 0;
                }
            }

            return false;
        }

        // Tries to solve the puzzle, then return true if its solved
        public bool SolvePuzzle()
        {
            return FindSolution();
        }
    }
}
