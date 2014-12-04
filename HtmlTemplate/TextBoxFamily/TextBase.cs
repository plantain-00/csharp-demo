using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Indentation.CSharp;

using Microsoft.Win32;

using Xceed.Wpf.Toolkit;

namespace HtmlTemplate.TextBoxFamily
{
    public abstract class TextBase
    {
        protected readonly string DefaultExtension;
        protected readonly string DefaultName;
        protected readonly ICSharpCode.AvalonEdit.TextEditor TextBox;
        protected string Path;
        protected SaveFileDialog Sfd;
        protected TextBase(ICSharpCode.AvalonEdit.TextEditor textBox, string defaultName, string defaultExtension)
        {
            TextBox = textBox;
            DefaultName = defaultName;
            DefaultExtension = defaultExtension;
            FileMirror = string.Empty;
            TextBox.TextArea.IndentationStrategy = new CSharpIndentationStrategy(TextBox.Options);
            var foldingStrategy = new BraceFoldingStrategy();
            var foldingManager = FoldingManager.Install(TextBox.TextArea);
            var foldingUpdateTimer = new DispatcherTimer
                                     {
                                         Interval = TimeSpan.FromSeconds(2)
                                     };
            foldingUpdateTimer.Tick += delegate
                                       {
                                           foldingStrategy.UpdateFoldings(foldingManager, TextBox.Document);
                                       };
            foldingUpdateTimer.Start();
            Strategies = new Dictionary<Key, Action<KeyEventArgs>>();
            Strategies.Add(Key.S,
                           async e =>
                                 {
                                     if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                     {
                                         Handle(e);
                                         if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                                         {
                                             await SaveAs();
                                         }
                                         else if (Path == null)
                                         {
                                             await SaveAs();
                                         }
                                         else
                                         {
                                             await Save();
                                         }
                                     }
                                 });
        }
        public Dictionary<Key, Action<KeyEventArgs>> Strategies { get; private set; }
        public string FileMirror { get; protected set; }
        public TextBase Handle(KeyEventArgs e)
        {
            e.Handled = true;
            return this;
        }
        protected virtual async Task<TextBase> Save()
        {
            try
            {
                using (var stream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    stream.SetLength(0);
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        await writer.WriteAsync(TextBox.Text);
                        FileMirror = TextBox.Text;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("保存失败！" + exception.Message);
            }
            return this;
        }
        protected virtual async Task<TextBase> SaveAs()
        {
            if (Sfd == null)
            {
                Sfd = new SaveFileDialog
                      {
                          Filter = DefaultExtension + "|文本文件|*.txt|所有文件|*.*",
                          FileName = DefaultName
                      };
            }
            try
            {
                if (Sfd.ShowDialog() == true)
                {
                    Path = Sfd.FileName;
                    await Save();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("保存失败！" + exception.Message);
            }
            return this;
        }
    }
}