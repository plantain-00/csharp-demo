using System.Windows;

namespace JsonAnalyser
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SingleInstanceAppliction.SetGuid("{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
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
