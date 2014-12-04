using System.Linq;
using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public sealed class CSharpRule : CodeRuleBase
    {
        public static readonly CSharpRule Instance = new CSharpRule();
        private CSharpRule()
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
                    if (line.Length == blankNumber + "{}".Length)
                    {
                        textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}", new string(' ', blankNumber))).Handle(e);
                    }
                    else
                    {
                        textBox.Remove(1).Insert(string.Format("\n{0}{{\n{1}", new string(' ', blankNumber), new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}", new string(' ', blankNumber))).Handle(e);
                    }
                }
                else
                {
                    textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab))).Handle(e);
                }
            }
        }
        public override void InputCloseBrace(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.IsLeft('{'))
            {
                var line = textBox.CurrentLine();
                var blankNumber = line.TakeWhile(c => c == ' ').Count();
                if (line.Length == blankNumber + "{".Length)
                {
                    textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}}}", new string(' ', blankNumber))).Handle(e);
                }
                else
                {
                    textBox.Remove(1).Insert(string.Format("\n{0}{{\n{1}", new string(' ', blankNumber), new string(' ', blankNumber + SpaceInTab)), string.Format("\n{0}}}", new string(' ', blankNumber))).Handle(e);
                }
            }
        }
        public override void InputSpace(TextEditor textBox, KeyEventArgs e)
        {
            if (!textBox.IsLeft(' ', 4)
                && textBox.IsLeft('<', 3)
                && textBox.IsLeft('<', 2)
                && textBox.IsLeft('='))
            {
                textBox.Remove(3).Insert(" <<= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 4)
                     && textBox.IsLeft('>', 3)
                     && textBox.IsLeft('>', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(3).Insert(" >>= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('&', 2)
                     && textBox.IsLeft('&'))
            {
                textBox.Remove(2).Insert(" && ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('|', 2)
                     && textBox.IsLeft('|'))
            {
                textBox.Remove(2).Insert(" || ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('<', 2)
                     && textBox.IsLeft('<'))
            {
                textBox.Remove(2).Insert(" << ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('>', 2)
                     && textBox.IsLeft('>'))
            {
                textBox.Remove(2).Insert(" >> ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('=', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" == ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('!', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" != ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('<', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" <= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('>', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" >= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('+', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" += ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('-', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" -= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('*', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" *= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('/', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" /= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('%', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" %= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('&', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" &= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('|', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" |= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('^', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(2).Insert(" ^= ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('-', 2)
                     && textBox.IsLeft('>'))
            {
                textBox.Remove(2).Insert(" -> ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('?', 2)
                     && textBox.IsLeft('?'))
            {
                textBox.Remove(2).Insert(" ?? ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 3)
                     && textBox.IsLeft('=', 2)
                     && textBox.IsLeft('>'))
            {
                textBox.Remove(2).Insert(" => ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('+'))
            {
                textBox.Remove(1).Insert(" + ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('-'))
            {
                textBox.Remove(1).Insert(" - ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('*'))
            {
                textBox.Remove(1).Insert(" * ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('/'))
            {
                textBox.Remove(1).Insert(" / ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('%'))
            {
                textBox.Remove(1).Insert(" % ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('&'))
            {
                textBox.Remove(1).Insert(" & ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('|'))
            {
                textBox.Remove(1).Insert(" | ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('^'))
            {
                textBox.Remove(1).Insert(" ^ ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('='))
            {
                textBox.Remove(1).Insert(" = ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('<'))
            {
                textBox.Remove(1).Insert(" < ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('>'))
            {
                textBox.Remove(1).Insert(" > ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft('?'))
            {
                textBox.Remove(1).Insert(" ? ").Handle(e);
            }
            else if (!textBox.IsLeft(' ', 2)
                     && textBox.IsLeft(':'))
            {
                textBox.Remove(1).Insert(" : ").Handle(e);
            }
            else
            {
                base.InputSpace(textBox, e);
            }
        }
        public override void InputSemicolon(TextEditor textBox, KeyEventArgs e)
        {
            if (!textBox.IsRight(' '))
            {
                textBox.Insert("; ").Handle(e);
            }
        }
    }
}