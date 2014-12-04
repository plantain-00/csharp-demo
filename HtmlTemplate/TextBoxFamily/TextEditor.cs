using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using HtmlTemplate.RuleFamily;

using ICSharpCode.AvalonEdit.CodeCompletion;

using Microsoft.Win32;

using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace HtmlTemplate.TextBoxFamily
{
    public class TextEditor : TextBase
    {
        private readonly IRule _originalRule;
        private OpenFileDialog _ofd;
        private IRule _rule;
        private CompletionWindow completionWindow;
        public TextEditor(ICSharpCode.AvalonEdit.TextEditor textBox, IRule originalRule, string defaultName, string defaultExtension)
            : base(textBox, defaultName, defaultExtension)
        {
            _rule = originalRule;
            _originalRule = originalRule;
            TextBox.TextArea.TextEntered += (sender, e) =>
                                            {
                                                if (e.Text == ".")
                                                {
                                                    // open code completion after the user has pressed dot:
                                                    completionWindow = new CompletionWindow(TextBox.TextArea);
                                                    // provide AvalonEdit with the data:
                                                    var data = completionWindow.CompletionList.CompletionData;
                                                    var typeName = GetTypeName();
                                                    var items = CompletionDataHelper.GetMembers(typeName);
                                                    if (items == null)
                                                    {
                                                        items = CompletionDataHelper.GetTypes(typeName);
                                                    }
                                                    if (items == null)
                                                    {
                                                        return;
                                                    }
                                                    foreach (var item in items)
                                                    {
                                                        data.Add(item);
                                                    }
                                                    completionWindow.Show();
                                                    completionWindow.Closed += delegate
                                                                               {
                                                                                   completionWindow = null;
                                                                               };
                                                }
                                            };
            TextBox.TextArea.TextEntering += (sender, e) =>
                                             {
                                                 if (e.Text.Length > 0
                                                     && completionWindow != null)
                                                 {
                                                     if (!char.IsLetterOrDigit(e.Text[0]))
                                                     {
                                                         completionWindow.CompletionList.RequestInsertion(e);
                                                     }
                                                 }
                                             };
            TextBox.PreviewKeyDown += (sender, e) =>
                                      {
                                          RefreshRule();
                                          if (Strategies.ContainsKey(e.Key))
                                          {
                                              Strategies[e.Key](e);
                                          }
                                      };
            Strategies.Add(Key.OemOpenBrackets,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputOpenBrace(this, e);
                               }
                               else
                               {
                                   _rule.InputOpenBracket(this, e);
                               }
                           });
            Strategies.Add(Key.D9,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputOpenParenthese(this, e);
                               }
                           });
            Strategies.Add(Key.Back, e => _rule.InputBackspace(this, e));
            Strategies.Add(Key.ImeProcessed, e => _rule.InputImeProcessed(this, e));
            Strategies.Add(Key.OemQuotes,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputDoubleQuotation(this, e);
                               }
                               else
                               {
                                   _rule.InputSingleQuotation(this, e);
                               }
                           });
            Strategies.Add(Key.Tab, e => _rule.InputTab(this, e));
            Strategies.Add(Key.Enter, e => _rule.InputEnter(this, e));
            Strategies.Add(Key.OemCloseBrackets,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputCloseBrace(this, e);
                               }
                               else
                               {
                                   _rule.InputCloseBracket(this, e);
                               }
                           });
            Strategies.Add(Key.OemMinus,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                               }
                               else
                               {
                                   _rule.InputMinus(this, e);
                               }
                           });
            Strategies.Add(Key.OemPlus,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputPlus(this, e);
                               }
                               else
                               {
                                   _rule.InputEquality(this, e);
                               }
                           });
            Strategies.Add(Key.OemPeriod,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputGreaterThan(this, e);
                               }
                               else
                               {
                                   _rule.InputPeriod(this, e);
                               }
                           });
            Strategies.Add(Key.OemSemicolon,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputColon(this, e);
                               }
                               else
                               {
                                   if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                   {
                                       if (SelectedMoreThanOneLine())
                                       {
                                           TabSelectedText("@:").Handle(e);
                                       }
                                       else
                                       {
                                           TabLine("@:").Handle(e);
                                       }
                                   }
                                   else
                                   {
                                       _rule.InputSemicolon(this, e);
                                   }
                               }
                           });
            Strategies.Add(Key.Multiply, e => _rule.InputMultiply(this, e));
            Strategies.Add(Key.Subtract, e => _rule.InputMinus(this, e));
            Strategies.Add(Key.Add, e => _rule.InputPlus(this, e));
            Strategies.Add(Key.Decimal, e => _rule.InputPeriod(this, e));
            Strategies.Add(Key.D8,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputMultiply(this, e);
                               }
                           });
            Strategies.Add(Key.Divide, e => _rule.InputDivide(this, e));
            Strategies.Add(Key.Oem2,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputQuestion(this, e);
                               }
                               else
                               {
                                   if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                                   {
                                       if (SelectedMoreThanOneLine())
                                       {
                                           TabSelectedText("//").Handle(e);
                                       }
                                       else
                                       {
                                           TabLine("//").Handle(e);
                                       }
                                   }
                                   else
                                   {
                                       _rule.InputDivide(this, e);
                                   }
                               }
                           });
            Strategies.Add(Key.D5,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputPercent(this, e);
                               }
                           });
            Strategies.Add(Key.D7,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputAnd(this, e);
                               }
                           });
            Strategies.Add(Key.OemBackslash,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputOr(this, e);
                               }
                               else
                               {
                                   _rule.InputBackslash(this, e);
                               }
                           });
            Strategies.Add(Key.D6,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputXor(this, e);
                               }
                           });
            Strategies.Add(Key.D1,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputExclamation(this, e);
                               }
                           });
            Strategies.Add(Key.Oem3,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputReverse(this, e);
                               }
                               else
                               {
                                   _rule.InputOem3(this, e);
                               }
                           });
            Strategies.Add(Key.OemComma,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputLessThan(this, e);
                               }
                               else
                               {
                                   _rule.InputComma(this, e);
                               }
                           });
            Strategies.Add(Key.D0,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputCloseParenthese(this, e);
                               }
                           });
            Strategies.Add(Key.D2,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputAt(this, e);
                               }
                           });
            Strategies.Add(Key.D3,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputSharp(this, e);
                               }
                           });
            Strategies.Add(Key.D4,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   _rule.InputDollar(this, e);
                               }
                           });
            Strategies.Add(Key.Space,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control)
                                   && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                               {
                                   if (SelectedMoreThanOneLine())
                                   {
                                       TabSelectedText(" ").Handle(e);
                                   }
                                   else
                                   {
                                       TabLine(" ").Handle(e);
                                   }
                               }
                               else
                               {
                                   _rule.InputSpace(this, e);
                               }
                           });
            Strategies.Add(Key.C,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   _rule.Copy(this, e);
                               }
                           });
            Strategies.Add(Key.X,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   _rule.Cut(this, e);
                               }
                           });
            Strategies.Add(Key.O,
                           async e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   if (_ofd == null)
                                   {
                                       _ofd = new OpenFileDialog
                                              {
                                                  Filter = DefaultExtension + "|文本文件|*.txt|所有文件|*.*",
                                                  FileName = DefaultName
                                              };
                                   }
                                   try
                                   {
                                       Handle(e);
                                       if (_ofd.ShowDialog() == true)
                                       {
                                           Path = _ofd.FileName;
                                           using (var stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                                           {
                                               using (var reader = new StreamReader(stream, Encoding.UTF8))
                                               {
                                                   TextBox.Text = await reader.ReadToEndAsync();
                                                   FileMirror = TextBox.Text;
                                               }
                                           }
                                       }
                                   }
                                   catch (Exception exception)
                                   {
                                       MessageBox.Show("打开失败！" + exception.Message);
                                   }
                               }
                           });
            Strategies.Add(Key.F,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   FindAndReplace.Instance.Show();
                                   FindAndReplace.Instance.ActivateTextBox();
                                   if (HasSelectedSomething())
                                   {
                                       FindAndReplace.Instance.first.Text = TextBox.SelectedText;
                                       FindAndReplace.Instance.first.SelectAll();
                                   }
                               }
                           });
            Strategies.Add(Key.H,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   FindAndReplace.Instance.Show();
                                   FindAndReplace.Instance.ActivateTextBox();
                                   if (HasSelectedSomething())
                                   {
                                       FindAndReplace.Instance.second.Text = TextBox.SelectedText;
                                       FindAndReplace.Instance.second.SelectAll();
                                   }
                               }
                           });
            Strategies.Add(Key.R,
                           e =>
                           {
                               if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                               {
                                   _rule.Redo(this);
                               }
                           });
        }
        public string GetRule()
        {
            if (_rule is CSharpRule)
            {
                return "C#";
            }
            if (_rule is JsonRule)
            {
                return "Json";
            }
            if (_rule is StringRule)
            {
                return "字符串";
            }
            if (_rule is HtmlRule)
            {
                return "Html";
            }
            if (_rule is CssRule)
            {
                return "Css";
            }
            if (_rule is JsRule)
            {
                return "Javascript";
            }
            if (_rule is RazorRule)
            {
                return "Razor";
            }
            return "未识别";
        }
        /// <summary>
        ///     在光标左侧插入leftString，在光标右侧插入rightString。
        /// </summary>
        /// <param name="leftString"></param>
        /// <param name="rightString"></param>
        /// <returns></returns>
        public TextEditor Insert(string leftString, string rightString = null)
        {
            if (HasSelectedSomething())
            {
                ClearSelectedText();
            }
            return InsertBase(leftString, rightString);
        }
        private TextEditor InsertBase(string leftString, string rightString = null)
        {
            var original = TextBox.SelectionStart;
            var final = original;
            var s = string.Empty;
            if (!string.IsNullOrEmpty(leftString))
            {
                s += leftString;
                final += leftString.Length;
            }
            if (!string.IsNullOrEmpty(rightString))
            {
                s += rightString;
            }
            TextBox.Text = TextBox.Text.Insert(original, s);
            TextBox.SelectionStart = final;
            return this;
        }
        /// <summary>
        ///     在光标左侧移除leftCount个字符，在光标右侧移除rightCount个字符。
        /// </summary>
        /// <param name="leftCount"></param>
        /// <param name="rightCount"></param>
        /// <returns></returns>
        public TextEditor Remove(int leftCount, int rightCount = 0)
        {
            if (leftCount < 0
                || rightCount < 0
                || (leftCount == 0 && rightCount == 0))
            {
                throw new Exception("左边和右边不能小于0，至少有一个大于0！");
            }
            var tmp = TextBox.SelectionStart - leftCount;
            TextBox.Text = TextBox.Text.Remove(tmp, leftCount + rightCount);
            TextBox.SelectionStart = tmp;
            return this;
        }
        /// <summary>
        ///     光标左侧第count个字符是不是c。
        /// </summary>
        /// <param name="c"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsLeft(char c, int count = 1)
        {
            if (count < 1)
            {
                return false;
            }
            if (TextBox.SelectionStart - count < 0
                || TextBox.SelectionStart - count >= TextBox.Text.Length)
            {
                return false;
            }
            return TextBox.Text[TextBox.SelectionStart - count] == c;
        }
        /// <summary>
        ///     光标左侧第count个字符开始的等长字符串是不是s。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsLeft(string s, int count = 1)
        {
            if (count < 1)
            {
                return false;
            }
            if (TextBox.SelectionStart - count < 0
                || TextBox.SelectionStart - count + s.Length - 1 >= TextBox.Text.Length)
            {
                return false;
            }
            return TextBox.Text.Substring(TextBox.SelectionStart - count, s.Length) == s;
        }
        /// <summary>
        ///     光标右侧第count个字符是不是c。
        /// </summary>
        /// <param name="c"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsRight(char c, int count = 1)
        {
            if (count < 1)
            {
                return false;
            }
            if (TextBox.SelectionStart + count - 1 < 0
                || TextBox.SelectionStart + count - 1 >= TextBox.Text.Length)
            {
                return false;
            }
            return TextBox.Text[TextBox.SelectionStart + count - 1] == c;
        }
        /// <summary>
        ///     光标右侧第count个字符开始的等长字符串是不是s。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool IsRight(string s, int count = 1)
        {
            if (count < 1)
            {
                return false;
            }
            if (TextBox.SelectionStart + count - 1 < 0
                || TextBox.SelectionStart + count - 1 + s.Length - 1 >= TextBox.Text.Length)
            {
                return false;
            }
            return TextBox.Text.Substring(TextBox.SelectionStart + count - 1, s.Length) == s;
        }
        public string CurrentLine()
        {
            var line = TextBox.Document.GetLineByNumber(CurrentRow() + 1);
            var lineText = TextBox.Document.GetText(line.Offset, line.Length);
            return lineText == null ? "" : lineText.TrimEnd('\n');
        }
        /// <summary>
        ///     从0开始的当前光标所在的行数。
        /// </summary>
        /// <returns></returns>
        public int CurrentRow()
        {
            return TextBox.Text.Take(TextBox.SelectionStart).Count(t => t == '\n');
        }
        /// <summary>
        ///     从0开始的当前光标所在的列数。
        /// </summary>
        /// <returns></returns>
        public int CurrentColumn()
        {
            for (var i = 1; i < TextBox.SelectionStart; i++)
            {
                if (IsLeft('\n', i))
                {
                    return i - 1;
                }
            }
            return TextBox.SelectionStart;
        }
        public TextEditor ClearSelectedText()
        {
            Remove(0, TextBox.SelectionLength);
            return this;
        }
        public bool HasSelectedSomething()
        {
            return TextBox.SelectionLength > 0;
        }
        public bool SelectedMoreThanOneLine()
        {
            return HasSelectedSomething() && TextBox.SelectedText.Contains('\n');
        }
        public TextEditor TabSelectedText(string s)
        {
            var end = TextBox.SelectionStart + TextBox.SelectionLength - 1;
            var length = TextBox.SelectionLength;
            var start = TextBox.SelectionStart;
            var column = CurrentColumn();
            for (var i = end; i >= start; i--)
            {
                if (TextBox.Text[i] == '\n')
                {
                    TextBox.Text = TextBox.Text.Insert(i + 1, s);
                    length += s.Length;
                }
            }
            TextBox.Text = TextBox.Text.Insert(start - column, s);
            TextBox.SelectionStart = start + s.Length;
            TextBox.SelectionLength = length;
            return this;
        }
        public TextEditor ShiftTabSelectedText()
        {
            var end = TextBox.SelectionStart + TextBox.SelectionLength - 1;
            var length = TextBox.SelectionLength;
            var start = TextBox.SelectionStart;
            var column = CurrentColumn();
            for (var i = end; i >= start; i--)
            {
                if (TextBox.Text[i] == '\n')
                {
                    var sum = TextBox.Text.Skip(i + 1).TakeWhile((c, j) => j < _rule.SpaceInTab && c == ' ').Count();
                    TextBox.Text = TextBox.Text.Remove(i + 1, sum);
                    length -= sum;
                }
            }
            var sum2 = TextBox.Text.Skip(start - column).TakeWhile((c, j) => j < _rule.SpaceInTab && c == ' ').Count();
            TextBox.Text = TextBox.Text.Remove(start - column, sum2);
            TextBox.SelectionStart = start - sum2;
            TextBox.SelectionLength = length;
            return this;
        }
        public bool IsLineStartWith(char c)
        {
            var line = CurrentLine().TrimStart(' ');
            if (line.Length == 0)
            {
                return false;
            }
            return line[0] == c;
        }
        public TextEditor TabLine(string s)
        {
            var start = TextBox.SelectionStart;
            var column = CurrentColumn();
            TextBox.Text = TextBox.Text.Insert(start - column, s);
            TextBox.SelectionStart = start + s.Length;
            return this;
        }
        public TextEditor ShiftTabLine()
        {
            var start = TextBox.SelectionStart;
            var column = CurrentColumn();
            var sum = TextBox.Text.Skip(start - column).TakeWhile((c, j) => j < _rule.SpaceInTab && c == ' ').Count();
            TextBox.Text = TextBox.Text.Remove(start - column, sum);
            TextBox.SelectionStart = start - sum;
            return this;
        }
        public void RefreshRule()
        {
            if (_originalRule == CSharpRule.Instance
                && IsLeft('@', 2)
                && IsLeft('{')
                && IsRight('}'))
            {
                _rule = RazorRule.Instance;
            }
            else
            {
                _rule = _originalRule;
                for (var i = 0; i < TextBox.SelectionStart; i++)
                {
                    var c = TextBox.Text[i];
                    if (_rule != StringRule.Instance
                        && c == '\"')
                    {
                        _rule = StringRule.Instance;
                    }
                    else if (_rule == StringRule.Instance
                             && c == '\"')
                    {
                        _rule = _originalRule;
                    }
                }
                if (_originalRule == CSharpRule.Instance
                    && _rule != StringRule.Instance
                    && IsLineStartWith('<'))
                {
                    _rule = HtmlRule.Instance;
                }
            }
        }
        public TextEditor CutCurrentLine()
        {
            var line = CurrentLine();
            var column = CurrentColumn();
            var row = CurrentRow();
            Clipboard.SetData(DataFormats.UnicodeText, CurrentLine());
            if (TextBox.LineCount == 0
                || TextBox.LineCount == 1)
            {
                TextBox.Clear();
            }
            else if (row == 0)
            {
                Remove(column, line.Length - column + 1);
            }
            else if (line.Length - column >= 0)
            {
                Remove(column + 1, line.Length - column);
            }
            return this;
        }
        public TextEditor CopyCurrentLine()
        {
            Clipboard.SetData(DataFormats.UnicodeText, CurrentLine());
            return this;
        }
        public async void OnClosing(CancelEventArgs e)
        {
            if (FileMirror != TextBox.Text)
            {
                var result = MessageBox.Show(string.Format("{0}内容尚未保存，是否保存？", DefaultName), "警告", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        await SaveAs();
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
        public void Redo()
        {
            TextBox.Redo();
        }
        private string GetTypeName()
        {
            for (var i = 0; i < TextBox.CaretOffset && TextBox.CaretOffset - i - 1 >= 0; i++)
            {
                var c = TextBox.Text[TextBox.CaretOffset - i - 1];
                if (!char.IsLetterOrDigit(c)
                    && c != '_'
                    && c != '.')
                {
                    return TextBox.Text.Substring(TextBox.CaretOffset - i, i - 1);
                }
            }
            return TextBox.Text.Substring(0, TextBox.CaretOffset - 1);
        }
    }
}