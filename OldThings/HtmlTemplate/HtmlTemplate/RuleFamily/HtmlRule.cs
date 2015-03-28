using System.Linq;
using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public sealed class HtmlRule : RuleBase
    {
        public static readonly HtmlRule Instance = new HtmlRule();
        private HtmlRule()
        {
        }
        public override void InputEnter(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.IsLeft('>')
                && textBox.IsRight('<')
                && textBox.IsRight('/', 2))
            {
                var line = textBox.CurrentLine();
                var blankNumber = line.TakeWhile(c => c == ' ').Count();
                textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}", new string(' ', blankNumber))).Handle(e);
            }
        }
        public override void InputGreaterThan(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.IsLineStartWith('<')
                && !textBox.IsLeft('/')
                && !textBox.IsLeft('-'))
            {
                var name = new string(textBox.CurrentLine().Trim(' ').Skip(1).TakeWhile(char.IsLetterOrDigit).ToArray());
                textBox.Insert(">", string.Format("</{0}>", name)).Handle(e);
            }
        }
        public override void InputEquality(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert(" = \"", "\"").Handle(e);
        }
        public override void InputMinus(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.IsLeft('<', 3)
                && textBox.IsLeft('!', 2)
                && textBox.IsLeft('-'))
            {
                textBox.Insert("-", "-->").Handle(e);
            }
            else
            {
                base.InputMinus(textBox, e);
            }
        }
    }
}