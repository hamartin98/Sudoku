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
        private bool inputAllowed = true;

        public MainWindow()
        {
            InitializeComponent();

            puzzle = new Puzzle(FileIO.readBoard("Puzzles/semiSolution.txt"));

            InitGird();
            initNumPad();
            SetGridData();

            puzzle.PuzzleSolved += PuzzleSolved;

        }

        // Triggered when the puzzle is fully solved
        private void PuzzleSolved(object sender, EventArgs args)
        {
            inputAllowed = false;
            MessageBox.Show("You solved the puzzle");
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

        // Initializes the numberPad
        private void initNumPad()
        {
            int num = 1;

            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    addNumPadButton(row, col, num.ToString(), 1);
                    num++;
                }
            }

            // add clear button
            addNumPadButton(3, 0, "Clear", 3);
        }

        // add a new button to the numPad with given parameters
        private void addNumPadButton(int row, int col, string value, int columnSpan)
        {
            Button btn;
            btn = new Button();
            btn.FontSize = 20;
            btn.Click += numPadBtnClicked;
            Grid.SetRow(btn, row);
            Grid.SetColumn(btn, col);
            Grid.SetColumnSpan(btn, columnSpan);
            btn.Content = value;
            numPad.Children.Add(btn);
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
                    numBtn = new NumberButton(puzzle.ValueAt(row, col), row, col);
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

        // occurs when a button is clicked on the numpad
        // sets the selected cells value on the sudoku to the value
        private void numPadBtnClicked(object sender, RoutedEventArgs e)
        {
            if(inputAllowed)
            {
                Button btn = sender as Button;
                string content = btn.Content.ToString();
                int value = 0;

                if (content != "Clear")
                {
                    value = Int32.Parse(content);
                }

                changeSelectedValue(value);
            }
        }

        // sets the value on the board and on the puzzle
        private void changeSelectedValue(int value)
        {
            if (selected != null)
            {
                int row = selected.Row;
                int col = selected.Col;
                puzzle.SetValue(value, row, col);
                selected.SetValue(value);
            }
        }
    }
}
