using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for NumberButton.xaml
    /// </summary>
    public partial class NumberButton : UserControl
    {
        private bool isSelected = false;
        private int value = 0;

        public delegate void NumberButtonSelectedEventHandler(object sender, EventArgs args);
        public event NumberButtonSelectedEventHandler ButtonSelected;

        public NumberButton(int value)
        {
            InitializeComponent();
            //this.DataContext = this;
            Update(value);
        }

        private void Update(int value)
        {
            this.value = value;
            btnNum.Content = value.ToString();
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Select();
        }

        // Select the button
        public void Select()
        {
            if(!isSelected)
            {
                OnButtonSelected();
                isSelected = true;
                btnNum.BorderBrush = new SolidColorBrush(Color.FromRgb(15, 60, 250));
                btnNum.BorderThickness = new Thickness(3, 3, 3, 3);
            }
        }

        // Deselect the button if other is selected
        public void Deselect()
        {
            if(isSelected)
            {
                isSelected = false;
                btnNum.BorderThickness = new Thickness(0);
            }
        }

        // Returns the value of this button
        public int getValue()
        {
            return this.value;
        }

        // Sets the value of the button
        public void SetValue(int value)
        {
            Update(value);
        }

        protected virtual void OnButtonSelected()
        {
            if(ButtonSelected != null)
            {
                ButtonSelected(this, EventArgs.Empty);
            }
        }
    }
}
