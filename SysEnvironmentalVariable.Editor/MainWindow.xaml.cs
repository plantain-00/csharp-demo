using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SysEnvironmentalVariable.Editor
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var path = Environment.GetEnvironmentVariable("PATH");
            path = path.Replace(";", ";\n");
            textBox.Text = path;
            textBox.Focus();
        }
        private void TextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S
                && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                e.Handled = true;
                var directories = textBox.Text.Split(';');
                foreach (var directory in directories)
                {
                    if (!Directory.Exists(directory.Trim()))
                    {
                        MessageBox.Show(string.Format("路径不存在:\n{0}", directory.Trim()));
                        return;
                    }
                }
                if (MessageBoxResult.OK == MessageBox.Show("确定要保存修改吗？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel))
                {
                    Environment.SetEnvironmentVariable("PATH", textBox.Text.Replace("\n", ""));
                }
            }
        }
    }
}