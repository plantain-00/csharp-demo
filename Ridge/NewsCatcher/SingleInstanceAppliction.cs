using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace NewsCatcher
{
    internal class SingleInstanceAppliction
    {
        private static Mutex _mutex;
        static SingleInstanceAppliction()
        {
            WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        }
        internal static int WM_SHOWME { get; }
        internal static void SetGuid(string guid)
        {
            _mutex = new Mutex(true, guid);
        }
        internal static void ReleaseMutex()
        {
            _mutex.ReleaseMutex();
        }
        internal static bool IsFirst()
        {
            return _mutex.WaitOne(TimeSpan.Zero, true);
        }
        [DllImport("user32")]
        private static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        private static extern int RegisterWindowMessage(string message);
        internal static void PostMessage()
        {
            PostMessage((IntPtr)0xffff, WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
        }
    }
}