using System.Linq;
using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public sealed class RazorRule : RuleBase
    {
        public static readonly RazorRule Instance = new RazorRule();
        private RazorRule()
        {
        }
        public override void InputEnter(TextEditor textBox, KeyEventArgs e)
        {
            var line = textBox.CurrentLine();
            var blankNumber = line.TakeWhile(c => c == ' ').Count();
            if (textBox.IsLeft('{'))
            {
                if (textBox.IsRight('}'))
                {
                    textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}", new string(' ', blankNumber))).Handle(e);
                }
                else
                {
                    textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab))).Handle(e);
                }
            }
            else
            {
                textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber))).Handle(e);
            }
        }
    }
}