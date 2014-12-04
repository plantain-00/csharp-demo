using System.ComponentModel;
using System.Windows.Media;

using MineSweeper.Interfaces;
using MineSweeper.Properties;

namespace MineSweeper
{
    internal class Result : IResult
    {
        private Brush _foreground;
        private bool _isOn;
        private int _mineNumber;
        private string _timePast;
        private int _undiggedNumber;
        public Result()
        {
            Reset();
        }
        public bool IsOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                _isOn = value;
                OnPropertyChanged("IsOn");
            }
        }
        public int UndiggedNumber
        {
            get
            {
                return _undiggedNumber;
            }
            set
            {
                _undiggedNumber = value;
                OnPropertyChanged("UndiggedNumber");
            }
        }
        public string TimePast
        {
            get
            {
                return _timePast;
            }
            set
            {
                _timePast = value;
                OnPropertyChanged("TimePast");
            }
        }
        public int MineNumber
        {
            get
            {
                return _mineNumber;
            }
            set
            {
                _mineNumber = value;
                OnPropertyChanged("MineNumber");
            }
        }
        public Brush Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
                OnPropertyChanged("Foreground");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Reset()
        {
            Foreground = Brushes.Black;
            MineNumber = Constants.TOTAL;
            TimePast = string.Empty;
            UndiggedNumber = Constants.TOTAL_NUMBER;
            IsOn = true;
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                        new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}