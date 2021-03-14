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

        public MainWindow()
        {
            InitializeComponent();
            initGird();
        }

        // Adds row and column definitions to equally align elements based on the size of the grid
        private void initGird()
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
    }
}
