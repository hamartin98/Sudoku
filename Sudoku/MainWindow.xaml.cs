using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            GridLength gridLength = new GridLength(0, GridUnitType.Auto);

            for (int idx = 0; idx < SIZE; idx++)
            {
                rowDef = new RowDefinition();
                rowDef.Height = gridLength;
                sudokuGrid.RowDefinitions.Add(rowDef);

                colDef = new ColumnDefinition();
                colDef.Width = gridLength;
                sudokuGrid.ColumnDefinitions.Add(colDef);
            }
        }

        // Sets the values on the board
        private void SetGridData()
        {
            NumberButton numBtn;

            for(int row = 0; row < SIZE; row++)
            {
                for(int col = 0; col < SIZE; col++)
                {
                    numBtn = new NumberButton(puzzle.ValueAt(row, col));
                    Grid.SetRow(numBtn, row);
                    Grid.SetColumn(numBtn, col);
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
