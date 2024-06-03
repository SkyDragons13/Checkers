using Checkers.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.ViewModel
{
    public class MainWindowViewModel
    {
        public ICommand NewGameCommand { get; }
        public ICommand SaveGameCommand {get;}
        public ICommand BackCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand StatisticsCommand { get; }
        public ICommand AboutCommand { get; }
        public bool IsMultipleJumpsEnabled { get; set; }
        public bool gameStarted;
        private BoardView boardView;
        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(ExecuteNewGameCommand);
            SaveGameCommand=new RelayCommand(ExecuteSaveGameCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            LoadGameCommand=new RelayCommand(ExecuteLoadGameCommand);
            StatisticsCommand=new RelayCommand(ExecuteStatisticsCommand);
            AboutCommand=new RelayCommand(ExecuteAboutCommand);
        }
        private void ExecuteAboutCommand(object parameter)
        {
            // Define the about information
            string creatorName = "Csere Cosmin-Andrei";
            string email = "cosmin.csere@student.unitbv.ro";
            string group = "10LF321";
            string gameDescription = "Am creat un simplu joc de dame folosind WPF.\n " +
                "In clasicul joc de dame , o piesa alba se poate muta doar pe diagonala in jos , iar o piesa rosie pe diagonala in sus.\n" +
                "Cand o piese ajunge pe randul opus fata de pozitia de start aceasta devine un rege si se poate muta pe fiecare diagonala\n" +
                "Pentru a interschimba intre jucatori am folosit butonul switch turn , iar toate verificarile legate de logica jocului au fost implementate\n" +
                "Jocul se termina atunci cand unul din jucatori ramane fara piese ";

            // Display about information in a MessageBox
            MessageBox.Show($"Creator: {creatorName}\nEmail: {email}\nGroup: {group}\n\nDescription: {gameDescription}", "About Checkers");
        }
        private void ExecuteStatisticsCommand(object parameter)
        {
            Tuple<int, int,int > wins = Helper.getWins();
            MessageBox.Show($"White has won {wins.Item1} times\n" +
                $"Red has won {wins.Item2} times\n" +
                $"The winner had at most {wins.Item3} pieces.");
        }
        private void ExecuteNewGameCommand(object parameter)
        {
            // Navigate to the BoardView page
            MainWindow mainWindow = (MainWindow) Application.Current.MainWindow;
            boardView = new BoardView(IsMultipleJumpsEnabled);
            mainWindow.MainFrame.Navigate(boardView);
        }
        private void ExecuteSaveGameCommand(object parameter) 
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Save the board as JSON to the selected file path
                string filePath = saveFileDialog.FileName;
                boardView.InitiateSave(filePath);
            }
        }
        private void ExecuteBackCommand(object parameter)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            boardView.Resume();
            mainWindow.MainFrame.Navigate(boardView);
        }
        private void ExecuteLoadGameCommand(object parameter)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                // Load the saved data from the selected file path
                string filePath = openFileDialog.FileName;
                SaveData saveData = Helper.Load(filePath);
                boardView=new BoardView(saveData);
                mainWindow.MainFrame.Navigate(boardView);
            }
        }

    }
}
