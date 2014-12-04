using System.IO;
using System.Linq;
using System.Windows;

namespace RenamingFile
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private string[] strArray;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var name in strArray.Where(s => s.Contains(textBox1.Text)))
            {
                new FileInfo(name).MoveTo(name.Replace(textBox1.Text, textBox2.Text));
            }
            Init();
        }
        private void Init()
        {
            strArray = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.TopDirectoryOnly);
            textBox.Clear();
            textBox1.Clear();
            textBox2.Clear();
            foreach (var s in strArray)
            {
                textBox.AppendText(new FileInfo(s).Name + "\n");
            }
        }
    }
}