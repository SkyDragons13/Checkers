using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Dama : INotifyPropertyChanged
    {

        public PieceColor PieceColor { get; set; }
        public bool IsKing { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Dama(PieceColor pieceColor, int row, int column, bool isKing = false)
        {
            PieceColor = pieceColor;
            IsKing = isKing;
            Row = row;
            Column = column;
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
    public enum PieceColor
    {
        White,
        Red
    }
}