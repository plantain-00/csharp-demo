using System.ComponentModel;
using System.Windows.Media;

namespace MineSweeper.Interfaces
{
    internal interface IResult : INotifyPropertyChanged
    {
        bool IsOn { get; set; }
        int UndiggedNumber { get; set; }
        string TimePast { get; set; }
        int MineNumber { get; set; }
        Brush Foreground { get; set; }
        void Reset();
    }
}