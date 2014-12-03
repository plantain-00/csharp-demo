using System;
using System.Windows;

using Microsoft.Win32;

namespace VideoPlayer
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private OpenFileDialog _openFileDialog;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog == null)
            {
                _openFileDialog = new OpenFileDialog
                                  {
                                      Filter = "avi文件|*.avi|mp3文件|*.mp3|wmv文件|*.wmv|所有文件|*.*"
                                  };
            }
            if (_openFileDialog.ShowDialog() == true)
            {
                mediaElement.Source = new Uri(_openFileDialog.FileName, UriKind.Relative);
            }
        }
    }
}