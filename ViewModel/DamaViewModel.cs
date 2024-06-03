using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

public class DamaViewModel : INotifyPropertyChanged
{
    private Brush _color;
    public Brush Color
    {
        get { return _color; }
        set
        {
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    private int _row;
    public int Row
    {
        get { return _row; }
        set
        {
            _row = value;
            OnPropertyChanged(nameof(Row));
        }
    }

    private int _column;
    public int Column
    {
        get { return _column; }
        set
        {
            _column = value;
            OnPropertyChanged(nameof(Column));
        }
    }
    private Visibility _displayR;
    public Visibility DisplayR
    {
        get { return _displayR; }
        set
        {
            _displayR = value;
            OnPropertyChanged(nameof(DisplayR));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}