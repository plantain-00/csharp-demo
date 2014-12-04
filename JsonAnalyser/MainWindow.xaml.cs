using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

using Newtonsoft.Json.Linq;

namespace JsonAnalyser
{
    public partial class MainWindow
    {
        private const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
            var hwnd = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(WndProc);
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
            if (msg == WM_DWMCOMPOSITIONCHANGED)
            {
                GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
                handled = true;
            }
            return IntPtr.Zero;
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var text = textBox.Text.Trim();
            if (text != string.Empty)
            {
                try
                {
                    if (text.StartsWith("["))
                    {
                        var source = JsonHeaderLogic.FromJToken(JArray.Parse(text));
                        treeView.ItemsSource = source.Children;
                    }
                    else
                    {
                        var source = JObject.Parse(text).Children().Select(JsonHeaderLogic.FromJToken).ToArray();
                        treeView.ItemsSource = source;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("wrong json.");
                }
            }
        }
    }
}