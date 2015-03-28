using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HtmlTemplate
{
    public sealed partial class FindAndReplace
    {
        public static readonly FindAndReplace Instance = new FindAndReplace();
        private readonly MainWindow _window = Application.Current.MainWindow as MainWindow;
        private FindAndReplace()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
        private void FindNext(object sender, RoutedEventArgs e)
        {
            var textBox = _window.CurrentTextBox();
            if (!string.IsNullOrEmpty(first.Text)
                && textBox != null)
            {
                Find(textBox);
            }
        }
        private int IndexOf(string s1, string s2, int start)
        {
            var comparison = caseSensitive.IsSelected ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            if (wordMatch.IsChecked == true)
            {
                while (start != -1)
                {
                    start = s1.IndexOf(s2, start, comparison);
                    if (start != -1)
                    {
                        if (IsWordMatch(s1, start, s2.Length))
                        {
                            return start;
                        }
                        start++;
                    }
                }
                return -1;
            }
            if (start >= s1.Length)
            {
                return -1;
            }
            return s1.IndexOf(s2, start, comparison);
        }
        private void Find(ICSharpCode.AvalonEdit.TextEditor textBox)
        {
            if (!Contains(textBox.Text, first.Text))
            {
                SetAsFailure();
                return;
            }
            var start = Equals(textBox.SelectedText, first.Text) ? textBox.CaretOffset + 1 : textBox.CaretOffset;
            var index = IndexOf(textBox.Text, first.Text, start);
            if (index == -1)
            {
                index = IndexOf(textBox.Text, first.Text, 0);
                if (index == -1)
                {
                    throw new Exception();
                }
            }
            textBox.Focus();
            textBox.SelectionStart = index;
            textBox.SelectionLength = first.Text.Length;
            textBox.ScrollToLine(textBox.Text.Take(textBox.SelectionStart).Count(t => t == '\n') + 1);
            SetAsSuccess();
        }
        private void Replace(ICSharpCode.AvalonEdit.TextEditor textBox)
        {
            if (Equals(textBox.SelectedText, first.Text))
            {
                var start = textBox.SelectionStart;
                textBox.Text = textBox.Text.Remove(start, textBox.SelectionLength).Insert(start, second.Text);
                textBox.SelectionLength = 0;
                textBox.SelectionStart = start + second.Text.Length;
                Find(textBox);
            }
            else
            {
                Find(textBox);
            }
        }
        private void ReplaceNext(object sender, RoutedEventArgs e)
        {
            var textBox = _window.CurrentTextBox();
            if (!string.IsNullOrEmpty(first.Text)
                && textBox != null)
            {
                Replace(textBox);
            }
        }
        private void ReplaceAll(object sender, RoutedEventArgs e)
        {
            var textBox = _window.CurrentTextBox();
            if (!string.IsNullOrEmpty(first.Text)
                && textBox != null)
            {
                if (!Contains(textBox.Text, first.Text))
                {
                    SetAsFailure();
                    return;
                }
                while (Contains(textBox.Text, first.Text))
                {
                    Replace(textBox);
                }
                textBox.Focus();
                SetAsSuccess();
            }
        }
        public void ActivateTextBox()
        {
            first.Focus();
            first.SelectAll();
        }
        private void SetAsNormal()
        {
            first.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        }
        private void SetAsSuccess()
        {
            first.Foreground = Brushes.Green;
        }
        private void SetAsFailure()
        {
            first.Foreground = Brushes.Red;
        }
        private void First_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            SetAsNormal();
        }
        private bool Equals(string s1, string s2)
        {
            if (caseSensitive.IsSelected)
            {
                return s1 == s2;
            }
            return s1.ToLower() == s2.ToLower();
        }
        private bool Contains(string s1, string s2)
        {
            if (caseSensitive.IsSelected)
            {
                if (wordMatch.IsChecked == true)
                {
                    var index = 0;
                    while (index != -1)
                    {
                        index = s1.IndexOf(s2, index);
                        if (index != -1)
                        {
                            if (IsWordMatch(s1, index, s2.Length))
                            {
                                return true;
                            }
                            index++;
                        }
                    }
                    return false;
                }
                return s1.Contains(s2);
            }
            if (wordMatch.IsChecked == true)
            {
                var index = 0;
                while (index != -1)
                {
                    index = s1.ToLower().IndexOf(s2.ToLower(), index);
                    if (index != -1)
                    {
                        if (IsWordMatch(s1.ToLower(), index, s2.ToLower().Length))
                        {
                            return true;
                        }
                        index++;
                    }
                }
                return false;
            }
            return s1.ToLower().Contains(s2.ToLower());
        }
        private bool IsWordMatch(string text, int start, int length)
        {
            if (wordMatch.IsChecked == true)
            {
                if (start != 0)
                {
                    if (char.IsLetterOrDigit(text[start - 1])
                        || text[start - 1] == '_')
                    {
                        return false;
                    }
                }
                if (start + length != text.Length)
                {
                    if (char.IsLetterOrDigit(text[start + length])
                        || text[start + length] == '_')
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
    }
}