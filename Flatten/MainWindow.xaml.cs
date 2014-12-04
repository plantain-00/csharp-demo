using System;
using System.IO;
using System.Windows;
using System.Windows.Interop;

using Microsoft.VisualBasic.FileIO;

namespace Flatten
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == SingleInstanceAppliction.WM_SHOWME)
            {
                if (WindowState == WindowState.Minimized)
                {
                    WindowState = WindowState.Normal;
                }
                var top = Topmost;
                Topmost = true;
                Topmost = top;
            }
            return IntPtr.Zero;
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_OnPreviewDrop(object sender, DragEventArgs e)
        {
            var directories = (string[]) e.Data.GetData(DataFormats.FileDrop);
            var destination = Path.GetDirectoryName(directories[0]);
            foreach (var directory in directories)
            {
                Move(directory, destination);
                textBox.Text += directory + "\n";
            }
        }
        private void TextBox_OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
        private static void Move(string path, string destination)
        {
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var name = Path.GetFileName(file);
                    var destinationPath = Path.Combine(destination, name);
                    if (File.Exists(destinationPath))
                    {
                        destinationPath = Path.Combine(destination, Guid.NewGuid() + name);
                    }
                    File.Move(file, destinationPath);
                }
                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    Move(directory, destination);
                }
                FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
        }
    }
}