using System.ComponentModel;
using System.Windows.Media;

using TakeColor.Annotations;

namespace TakeColor
{
    public class Model : INotifyPropertyChanged
    {
        private SolidColorBrush _background;
        private string _text;
        private SolidColorBrush _foreground;
        public Model()
        {
            Background = Brushes.Black;
            Text = "none";
            Foreground = Brushes.Black;
        }
        public SolidColorBrush Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
                if (_background.Color.R + _background.Color.G + _background.Color.B < 383)
                {
                    Foreground = Brushes.White;
                }
                else
                {
                    Foreground = Brushes.Black;
                }
                OnPropertyChanged("Background");
            }
        }
        public SolidColorBrush Foreground
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
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}