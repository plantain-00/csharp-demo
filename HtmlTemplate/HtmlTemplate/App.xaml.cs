using System.Windows;

namespace HtmlTemplate
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SingleInstanceAppliction.SetGuid("{7FF62BFA-CFF0-41C9-A35C-664DF3013BBA}");
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
