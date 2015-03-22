using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Xml;

using HtmlTemplate.RuleFamily;
using HtmlTemplate.TextBoxFamily;

using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

using Newtonsoft.Json;

using RazorEngine;

using TextReader = HtmlTemplate.TextBoxFamily.TextReader;

namespace HtmlTemplate
{
    public partial class MainWindow
    {
        private readonly TextEditor _dataEditor;
        private readonly TextReader _resultReader;
        private readonly TextEditor _stringEditor;
        private readonly TextEditor _templateEditor;
        public MainWindow()
        {
            using (var s = typeof(MainWindow).Assembly.GetManifestResourceStream("HtmlTemplate.Razor-Mode.xshd"))
            {
                if (s == null)
                {
                    throw new InvalidOperationException("Could not find embedded resource");
                }
                using (XmlReader reader = new XmlTextReader(s))
                {
                    HighlightingManager.Instance.RegisterHighlighting("Razor",
                                                                      new[]
                                                                      {
                                                                          ".cool"
                                                                      },
                                                                      HighlightingLoader.Load(reader, HighlightingManager.Instance));
                }
            }
            using (var s = typeof(MainWindow).Assembly.GetManifestResourceStream("HtmlTemplate.Json-Mode.xshd"))
            {
                if (s == null)
                {
                    throw new InvalidOperationException("Could not find embedded resource");
                }
                using (XmlReader reader = new XmlTextReader(s))
                {
                    HighlightingManager.Instance.RegisterHighlighting("Json",
                                                                      new[]
                                                                      {
                                                                          ".cool"
                                                                      },
                                                                      HighlightingLoader.Load(reader, HighlightingManager.Instance));
                }
            }

            InitializeComponent();
            _dataEditor = new TextEditor(data, JsonRule.Instance, "file1", "数据文件|*.data");
            _templateEditor = new TextEditor(template, CSharpRule.Instance, "file1", "模板文件|*.template");
            _stringEditor = new TextEditor(@string, StringRule.Instance, "file1", "字符串文件|*.string");
            _resultReader = new TextReader(result, "file1", "结果文件|*.result");
            template.Focus();
            var timer = new DispatcherTimer();
            timer.Tick += delegate
                          {
                              tabData.Header = data.Text == _dataEditor.FileMirror ? "数据  " : "数据 *";
                              tabTemplate.Header = template.Text == _templateEditor.FileMirror ? "模板  " : "模板 *";
                              tabString.Header = @string.Text == _stringEditor.FileMirror ? "字符串  " : "字符串 *";
                              resultHeader.Content = result.Text == _resultReader.FileMirror ? "结果  " : "结果 *";
                          };
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
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
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            tabResult.IsSelected = true;
            var templateText = template.Text.Trim();
            if (string.IsNullOrEmpty(templateText))
            {
                result.Text = "模板不能为空！";
                return;
            }
            var dataText = data.Text.Trim();
            if (string.IsNullOrEmpty(dataText))
            {
                dataText = "{}";
            }
            try
            {
                result.Text = "正在处理数据...\n\n";
                var jDynamic = await ParseDataAsync(dataText);
                result.Text += "正在处理模板...\n\n";
                result.Text = await ParseTemplateAsync(templateText, jDynamic, @string.Text);
            }
            catch (Exception exception)
            {
                result.Text += exception.Message;
            }
        }
        private static Task<dynamic> ParseDataAsync(string data)
        {
            return Task.Factory.StartNew(delegate
                                         {
                                             try
                                             {
                                                 return JsonConvert.DeserializeObject<dynamic>(data); 
                                             }
                                             catch (Exception exception)
                                             {
                                                 throw new Exception("数据格式错误！\n\n" + exception.Message);
                                             }
                                         });
        }
        private static Task<string> ParseTemplateAsync(string template, object @object, string text)
        {
            return Task.Factory.StartNew(() =>
                                         {
                                             try
                                             {
                                                 return Razor.Parse(template,
                                                                    new
                                                                    {
                                                                        Json = @object,
                                                                        String = text
                                                                    });
                                             }
                                             catch (Exception exception)
                                             {
                                                 throw new Exception("模板错误！\n\n" + exception.Message);
                                             }
                                         });
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            FindAndReplace.Instance.Hide();
            _dataEditor.OnClosing(e);
            _templateEditor.OnClosing(e);
            _stringEditor.OnClosing(e);
            if (!e.Cancel)
            {
                Application.Current.Shutdown();
            }
        }
        public TextEditor CurrentEditor()
        {
            if (tabTemplate.IsSelected)
            {
                return _templateEditor;
            }
            if (tabString.IsSelected)
            {
                return _stringEditor;
            }
            return tabData.IsSelected ? _dataEditor : null;
        }
        public ICSharpCode.AvalonEdit.TextEditor CurrentTextBox()
        {
            if (tabTemplate.IsSelected)
            {
                return template;
            }
            if (tabString.IsSelected)
            {
                return @string;
            }
            return tabData.IsSelected ? data : null;
        }
    }
}