using System;
using System.Diagnostics;
using System.Windows.Input;

namespace HtmlTemplate.TextBoxFamily
{
    public class TextReader : TextBase
    {
        public TextReader(ICSharpCode.AvalonEdit.TextEditor textBox, string defaultName, string defaultExtension)
            : base(textBox, defaultName, defaultExtension)
        {
            TextBox.PreviewKeyDown += (sender, e) =>
                                      {
                                          if (Strategies.ContainsKey(e.Key))
                                          {
                                              Strategies[e.Key](e);
                                          }
                                      };
            Strategies.Add(Key.B,
                           async e =>
                                 {
                                     if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                     {
                                         e.Handled = true;
                                         var path = System.IO.Path.GetTempPath();
                                         Path = System.IO.Path.Combine(path, Guid.NewGuid() + ".htm");
                                         await Save();
                                         Process.Start(new ProcessStartInfo(WindowsHelper.GetDefaultBrowser(), Path));
                                     }
                                 });
        }
    }
}