using System.Linq;
using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public sealed class JsonRule : CodeRuleBase
    {
        public static readonly JsonRule Instance = new JsonRule();
        private JsonRule()
        {
        }
        public override void InputColon(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert(" : \"", "\"").Handle(e);
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
                        textBox.Insert(string.Format("\n{0}\"", new string(' ', blankNumber + SpaceInTab)), string.Format("\"\n{0}", new string(' ', blankNumber))).Handle(e);
                    }
                    else
                    {
                        textBox.Remove(1).Insert(string.Format("\n{0}{{\n{1}\"", new string(' ', blankNumber), new string(' ', blankNumber + SpaceInTab)), string.Format("\"\n{0}", new string(' ', blankNumber))).Handle(e);
                    }
                }
                else
                {
                    textBox.Insert(string.Format("\n{0}", new string(' ', blankNumber + SpaceInTab))).Handle(e);
                }
            }
            else if (textBox.IsLeft('['))
            {
                if (textBox.IsRight(']'))
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