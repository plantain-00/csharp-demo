using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public abstract class CodeRuleBase : RuleBase
    {
        public override void InputSingleQuotation(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert("", "'");
        }
        public override void InputDoubleQuotation(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert("", "\"");
        }
        public override void InputComma(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert(", ").Handle(e);
        }
        public override void InputSpace(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.IsLeft('{')
                && textBox.IsRight('}'))
            {
                textBox.Insert("", " ");
            }
        }
    }
}