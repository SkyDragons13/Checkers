using Checkers.Model;
using Checkers.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Controls;
using System;

namespace Checkers.Services
{
    public class Helper
    {
        public static Tuple<int,int,int> readWins()
        {
            int whiteWins = 0;
            int redWins = 0;
            int maxPieces = 0;
            string path = "Wins.txt";
            try
            {
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);
                    if (lines.Length >= 3)
                    {
                        int.TryParse(lines[0], out whiteWins);
                        int.TryParse(lines[1], out redWins);
                        int.TryParse(lines[2], out maxPieces);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading wins: " + ex.Message);
            }
            return Tuple.Create(whiteWins, redWins,maxPieces);
        }
        public static Tuple<int,int,int>getWins()
        {
            return (readWins());
        }
        public static void setWins(int whiteWins,int redWins,int maxPieces)
        {
            string path = "Wins.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(whiteWins);
                    writer.WriteLine(redWins);
                    writer.WriteLine(maxPieces);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing wins: " + ex.Message);
            }
        }
        public static ObservableCollection<DamaViewModel> InitializePieces(Board board)
        {
            ObservableCollection<DamaViewModel> Pieces = new ObservableCollection<DamaViewModel>();

            for (int row = 0; row < board.BoardMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < board.BoardMatrix.GetLength(1); col++)
                {
                    Dama dama = board.BoardMatrix[row, col];
                    if (dama != null)
                    {
                        DamaViewModel piece = new DamaViewModel();
                        piece.Color = dama.PieceColor == PieceColor.White ? Brushes.White : Brushes.Red;
                        piece.Row = row;
                        piece.Column = col;
                        if(!dama.IsKing)
                            piece.DisplayR = Visibility.Collapsed;
                        else
                            piece.DisplayR = Visibility.Visible;
                        Pieces.Add(piece);
                    }
                }
            }

            return Pieces;
        }
        public static ObservableCollection<BorderViewModel>InitializeBorders()
        {
            ObservableCollection<BorderViewModel> Cells = new ObservableCollection<BorderViewModel>();
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Brush background = (row + column) % 2 == 0 ? new SolidColorBrush(Color.FromRgb(210, 180, 140)) : new SolidColorBrush(Color.FromRgb(139, 69, 19));
                    Cells.Add(new BorderViewModel { Background = background ,Column=column,Row=row});
                }
            }
            return Cells;
        }
        public static void DefaultBorders(ObservableCollection<BorderViewModel> border)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Brush background = (row + column) % 2 == 0 ? new SolidColorBrush(Color.FromRgb(210, 180, 140)) : new SolidColorBrush(Color.FromRgb(139, 69, 19));
                    int index = row * 8 + column;
                    border[index].Background = background;
                }
            }
        }
        public static void Save(string filePath,Board board, bool jumpsEnabled, bool playerTurn)
        {
            var data = new
            {
                Board = board,
                JumpsEnabled = jumpsEnabled,
                PlayerTurn = playerTurn
            };
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }
        public static SaveData Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
            return data;
        }
        public static bool LoadCheckboxState(string path)
        {
            if (File.Exists(path))
            {
                string state = File.ReadAllText(path);
                if (state.Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
