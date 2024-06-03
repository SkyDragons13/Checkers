using Checkers.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Checkers.Services;
using System.Windows;
using System.Windows.Navigation;

namespace Checkers.ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BorderViewModel> Cells { get; set; }
        public ObservableCollection<DamaViewModel> Pieces { get; set; }
        private Board board;
        private DamaViewModel currentPiece;
        private GameBusinessLogic bl;
        private bool jumpsEnabled;
        private bool stop;
        public BoardViewModel()
        {
            Cells = Helper.InitializeBorders();
            board = new Board();
            bl= new GameBusinessLogic();
            Pieces = Helper.InitializePieces(board);
           
        }
        public void Load(SaveData saveData)
        {
            board = new Board();
            board=saveData.Board;
            jumpsEnabled =saveData.JumpsEnabled;
            if (bl.getPlayerTurn() != saveData.PlayerTurn)
                bl.SwitchTurn();
            bl.setCanJump(jumpsEnabled);
            Pieces=Helper.InitializePieces(board);
            bl.CountPieces(Pieces);
            OnPropertyChanged(nameof(board));
            OnPropertyChanged(nameof(jumpsEnabled));
            OnPropertyChanged(nameof(Pieces));

        }
        public void Start()
        {
            stop = false;
        }
        public void Save(string filePath)
        {
            Helper.Save(filePath, board, jumpsEnabled, bl.getPlayerTurn());

        }
        public void PieceClick(DamaViewModel piece)
        {
            if(stop)
                return;
            if((bl.getPlayerTurn() && piece.Color==Brushes.Red) || (!bl.getPlayerTurn() && piece.Color==Brushes.White))
            {
                MessageBox.Show("That is not your piece");
                return;
            }
            if (bl.getPlayerHasMoved() && !bl.getPlayerJumped())
            {
                MessageBox.Show("You have already moved.");
                Helper.DefaultBorders(Cells);
                return;
            }
            Helper.DefaultBorders(Cells);
            currentPiece = piece;
            List<Tuple<int, int>> validMoves=bl.GetValidMoves(board.GetDamaAtPosition(piece.Row, piece.Column),board,Pieces);
            bl.Moves(validMoves, Cells);
        }
        public void BorderClick(BorderViewModel border)
        {
            if (stop)
                return;
            if (border.Background != Brushes.Green)
                return;
            bl.UpdateBoard(board, border, currentPiece, Cells,Pieces);
        }
        public int SwitchButtonClick()
        {
            if (stop)
                return -1;
            if (!bl.getPlayerHasMoved())
            {
                MessageBox.Show("You haven't moved.");
                return 3;
            }
            bl.SwitchTurn();
            if (bl.getPlayerTurn())
                return 1;
            return 2;
        }
        public void HelpClick()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            stop = true;
            mainWindow.Show();
            mainWindow.Content.Visibility = Visibility.Visible;
            mainWindow.SaveButton.Visibility = Visibility.Collapsed;
            mainWindow.OpenButton.Visibility = Visibility.Collapsed;
            mainWindow.StatButton.Visibility = Visibility.Collapsed;
            mainWindow.NewButton.Visibility = Visibility.Collapsed;
            mainWindow.Checkbox.Visibility = Visibility.Collapsed;
            mainWindow.BackButton.Visibility = Visibility.Visible;
            mainWindow.AboutButton.Visibility = Visibility.Visible;
        }
        public void FileClick()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            stop = true;
            mainWindow.Show();
            mainWindow.Content.Visibility = Visibility.Visible;
            mainWindow.SaveButton.Visibility = Visibility.Visible;
            mainWindow.BackButton.Visibility = Visibility.Visible;
        }
        public void JumpsEnabled(bool jumps)
        {
            jumpsEnabled = jumps;
            bl.setCanJump(jumpsEnabled);
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
