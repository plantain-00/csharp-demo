using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace CopyWithPath
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
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
        private void TextBox_OnPreviewDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                textBox.Text += file + "\n";
                var path = Path.Combine(Directory.GetCurrentDirectory(), new string(file.Skip(3).ToArray()));
                FileHelper.CopyThoughDirectoryNotFound(file, path);
            }
        }
        private void TextBox_OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
    }
}