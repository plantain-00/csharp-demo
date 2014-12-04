using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace JsonAnalyser
{
    public class GlassHelper
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();
        public static bool ExtendGlassFrame(Window window, Thickness margin)
        {
            if (!DwmIsCompositionEnabled())
            {
                return false;
            }
            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
            {
                throw new InvalidOperationException("The Window must be shown before extending glass.");
            }
            window.Background = Brushes.Transparent;
            HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;
            var margins = new MARGINS(margin);
            DwmExtendFrameIntoClientArea(hwnd, ref margins);
            return true;
        }
    }
}