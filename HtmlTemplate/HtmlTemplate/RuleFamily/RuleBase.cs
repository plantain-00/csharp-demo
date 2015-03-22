using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public abstract class RuleBase : IRule
    {
        protected RuleBase()
        {
            SpaceInTab = 4;
        }
        public int SpaceInTab { get; set; }
        public virtual void InputOpenBrace(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert("", "}");
        }
        public virtual void InputOpenBracket(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert("", "]");
        }
        public virtual void InputOpenParenthese(TextEditor textBox, KeyEventArgs e)
        {
            textBox.Insert("", ")");
        }
        public virtual void InputImeProcessed(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputSingleQuotation(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputDoubleQuotation(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputBackspace(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.HasSelectedSomething())
            {
                textBox.ClearSelectedText().Handle(e);
            }
            else if ((textBox.IsLeft('{') && textBox.IsRight('}'))
                     || (textBox.IsLeft('[') && textBox.IsRight(']'))
                     || (textBox.IsLeft('(') && textBox.IsRight(')'))
                     || (textBox.IsLeft('\"') && textBox.IsRight('\"') && !textBox.IsLeft('\"', 2))
                     || (textBox.IsLeft('\'') && textBox.IsRight('\'') && !textBox.IsLeft('\'', 2)))
            {
                textBox.Remove(1, 1).Handle(e);
            }
            else if (textBox.IsLeft(new string(' ', SpaceInTab), SpaceInTab))
            {
                textBox.Remove(SpaceInTab).Handle(e);
            }
        }
        public virtual void InputTab(TextEditor textBox, KeyEventArgs e)
        {
            if (textBox.SelectedMoreThanOneLine())
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    textBox.ShiftTabSelectedText().Handle(e);
                }
                else
                {
                    textBox.TabSelectedText(new string(' ', SpaceInTab)).Handle(e);
                }
            }
            else
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    textBox.ShiftTabLine().Handle(e);
                }
                else
                {
                    textBox.TabLine(new string(' ', SpaceInTab)).Handle(e);
                }
            }
        }
        public virtual void InputEnter(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputMinus(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputPlus(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputEquality(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputCloseBrace(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputPeriod(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputGreaterThan(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputColon(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputSemicolon(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputMultiply(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputDivide(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputQuestion(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputPercent(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputAnd(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputBackslash(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputOr(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputXor(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputExclamation(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputReverse(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputOem3(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputLessThan(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputComma(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputCloseBracket(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputCloseParenthese(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputAt(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputSharp(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputDollar(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void InputSpace(TextEditor textBox, KeyEventArgs e)
        {
        }
        public virtual void Copy(TextEditor textBox, KeyEventArgs e)
        {
            if (!textBox.HasSelectedSomething())
            {
                textBox.CopyCurrentLine().Handle(e);
            }
        }
        public virtual void Cut(TextEditor textBox, KeyEventArgs e)
        {
            if (!textBox.HasSelectedSomething())
            {
                textBox.CutCurrentLine().Handle(e);
            }
        }
        public virtual void Redo(TextEditor textBox)
        {
            textBox.Redo();
        }
    }
}