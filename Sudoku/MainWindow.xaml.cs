using System;
using System.Windows;
using System.Windows.Controls;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int SIZE = 9;
        private Puzzle puzzle;
        NumberButton selected = null;

        public MainWindow()
        {
            InitializeComponent();

            puzzle = new Puzzle(FileIO.readBoard("Puzzles/puzzle1.txt"));

            InitGird();
            SetGridData();

        }

        // Adds row and column definitions to equally align elements based on the size of the grid
        private void InitGird()
        {
            RowDefinition rowDef;
            ColumnDefinition colDef;
            GridLength gridLength = new GridLength(64, GridUnitType.Pixel);
            GridLength gridLengthFix = new GridLength(10, GridUnitType.Pixel);
            GridLength currLength;

            // every fourth row and column will be empty to seperate boxes
            for (int idx = 0; idx < SIZE + 2; idx++)
            {
                currLength = idx == 3 || idx == 7 ? gridLengthFix : gridLength;

                rowDef = new RowDefinition();
                rowDef.Height = currLength;
                sudokuGrid.RowDefinitions.Add(rowDef);

                colDef = new ColumnDefinition();
                colDef.Width = currLength;
                sudokuGrid.ColumnDefinitions.Add(colDef);
            }
        }

        // Sets the values on the board
        private void SetGridData()
        {
            NumberButton numBtn;
            int actualRow;
            int actualCol;

            // we need to skip every fourth row and col
            for(int row = 0; row < SIZE; row++)
            {
                actualRow = row + row / 3;
                for (int col = 0; col < SIZE; col++)
                {
                    actualCol = col + col / 3;
                    numBtn = new NumberButton(puzzle.ValueAt(row, col));
                    numBtn.Width = 64;
                    numBtn.Height = 64;
                    Grid.SetRow(numBtn, actualRow);
                    Grid.SetColumn(numBtn, actualCol);
                    numBtn.ButtonSelected += SelectedChanged;
                    sudokuGrid.Children.Add(numBtn);
                }
            }
        }

        // Select the new button and deselect the old one
        public void SelectedChanged(object sender, EventArgs e)
        {
            if (selected != null)
            {
                selected.Deselect();
            }
            selected = sender as NumberButton;
        }
    }
}
