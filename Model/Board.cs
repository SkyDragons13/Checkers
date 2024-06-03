using System.Collections.Generic;
using System.ComponentModel;

namespace Checkers.Model
{
    public class Board : INotifyPropertyChanged
    {
        private Dama[,] board;

        public Dama[,] BoardMatrix
        {
            get { return board; }
            set
            {
                board = value;
                OnPropertyChanged("BoardMatrix");
            }
        }

        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Initialize the board with starting positions of Dama pieces
            BoardMatrix = new Dama[8, 8];

            // Place white pieces
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col += 2)
                {
                    if (row % 2 == 0)
                        BoardMatrix[row, col + 1] = new Dama(PieceColor.White,row,col+1);
                    else
                        BoardMatrix[row, col] = new Dama(PieceColor.White, row, col);
                }
            }

            // Place red pieces
            for (int row = 5; row < 8; row++)
            {
                for (int col = 0; col < 8; col += 2)
                {
                    if (row % 2 == 0)
                        BoardMatrix[row, col + 1] = new Dama(PieceColor.Red, row, col + 1);
                    else
                        BoardMatrix[row, col] = new Dama(PieceColor.Red, row, col);
                }
            }
        }
        public Dama GetDamaAtPosition(int row, int column)
        {
            return BoardMatrix[row, column];
        }
        public void RemoveDama(int row , int column)
        {
            BoardMatrix[row, column] = null;
        }
        public void MoveDama(int row, int column,int newRow,int newColumn)
        {
            Dama dama = BoardMatrix[row, column];
            BoardMatrix[row, column] = null;
            BoardMatrix[newRow, newColumn] = dama;
            dama.Column = newColumn;
            dama.Row = newRow;

            OnPropertyChanged("BoardMatrix");
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
