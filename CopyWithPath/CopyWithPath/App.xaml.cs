using System.Windows;

namespace CopyWithPath
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SingleInstanceAppliction.SetGuid("{99024CE7-CCC8-4CCA-BE2F-99791705097E}");
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
