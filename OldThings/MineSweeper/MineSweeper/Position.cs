using System;
using System.ComponentModel;
using System.Windows.Media;

using MineSweeper.Interfaces;
using MineSweeper.Properties;

namespace MineSweeper
{
    internal class Position : IPosition
    {
        private Brush _foreground;
        private bool _isUndigged;
        private string _text;
        public Position()
        {
            IsUndigged = true;
            Foreground = Brushes.Black;
            Text = string.Empty;
        }
        public int Value { get; set; }
        public Brush Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                if (Equals(value,
                           _foreground))
                {
                    return;
                }
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
                if (value == _text)
                {
                    return;
                }
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public bool IsUndigged
        {
            get
            {
                return _isUndigged;
            }
            set
            {
                _isUndigged = value;
                OnPropertyChanged("IsUndigged");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsNumber()
        {
            if (Text == String.Empty)
            {
                return false;
            }
            return Text != Constants.MINE_SYMBOL;
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