using System.Windows;

namespace Flatten
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SingleInstanceAppliction.SetGuid("{313C052C-6AB5-45F4-A689-1F1D51D4B96E}");
            if (SingleInstanceAppliction.IsFirst())
            {
                base.OnStartup(e);
                SingleInstanceAppliction.ReleaseMutex();
            }
            else
            {
                SingleInstanceAppliction.PostMessage();
                Shutdown();
            }
        }
    }
}
