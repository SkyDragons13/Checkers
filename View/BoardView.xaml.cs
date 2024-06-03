using Checkers.Model;
using Checkers.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
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

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Page
    {
        public BoardView(bool jumps)
        {
            InitializeComponent();
            ((BoardViewModel)DataContext).JumpsEnabled(jumps);
        }
        public BoardView(SaveData saveData)
        {
            InitializeComponent();
            ((BoardViewModel)DataContext).Load(saveData);
        }
        public void Resume()
        {
            ((BoardViewModel)DataContext).Start();
        }
        public void InitiateSave(string filepath)
        {
            ((BoardViewModel)DataContext).Save(filepath);
        }
        private void PieceClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked ellipse
            Ellipse clickedEllipse = sender as Ellipse;
            if (clickedEllipse != null)
            {
                // Get the DataContext of the ellipse, which should be a DamaViewModel
                DamaViewModel clickedPiece = clickedEllipse.DataContext as DamaViewModel;
                if (clickedPiece != null)
                {
                    // Call the PieceClick method in BoardViewModel
                    ((BoardViewModel)DataContext).PieceClick(clickedPiece);
                }
            }
        }
        private void BorderClick(object sender, MouseButtonEventArgs e)
        {
            // Get the clicked border
            Border clickedBorder = sender as Border;
            if (clickedBorder != null)
            {
                // Get the DataContext of the border, which should be a BorderViewModel
                BorderViewModel clickedCell = clickedBorder.DataContext as BorderViewModel;
                if (clickedCell != null)
                {
                    // Call the PieceClick method in BoardViewModel with the clicked piece
                    ((BoardViewModel)DataContext).BorderClick(clickedCell);
                }
            }
        }
        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            ((BoardViewModel)DataContext).HelpClick();
        }
        private void FileButtonClick(object sender, RoutedEventArgs e)
        {
            ((BoardViewModel)DataContext).FileClick();
        }
        private void SwitchButtonClick(object sender,RoutedEventArgs e)
        {
            int player=((BoardViewModel)DataContext).SwitchButtonClick();
            string colour;
            if (player == 3)
                return;
            if (player == 1)
                colour = "White turn";
            else
                colour = "Red turn";
            WinnerLabel.Content = $"{colour}";
            WinnerLabel.Foreground = player==1 ? Brushes.Black : Brushes.Red;
        }
    }
}
