using System.ComponentModel;
using System.Windows.Media;

namespace MineSweeper.Interfaces
{
    internal interface IPosition : INotifyPropertyChanged
    {
        int Value { get; set; }
        Brush Foreground { get; set; }
        string Text { get; set; }
        bool IsUndigged { get; set; }
        bool IsNumber();
    }
}