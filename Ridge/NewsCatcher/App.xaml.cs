using System.Windows;

namespace NewsCatcher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SingleInstanceAppliction.SetGuid("{798ACBC8-A899-4E5B-90D1-2F8E40EFFFB4}");
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
