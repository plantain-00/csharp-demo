using System.Windows.Input;

using HtmlTemplate.TextBoxFamily;

namespace HtmlTemplate.RuleFamily
{
    public interface IRule
    {
        int SpaceInTab { get; set; }
        /// <summary>
        ///     {
        /// </summary>
        /// <returns></returns>
        void InputOpenBrace(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     [
        /// </summary>
        /// <returns></returns>
        void InputOpenBracket(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     (
        /// </summary>
        /// <returns></returns>
        void InputOpenParenthese(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     输入法处理过的输入
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputImeProcessed(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     '
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputSingleQuotation(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     "
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputDoubleQuotation(TextEditor textBox, KeyEventArgs e);
        void InputBackspace(TextEditor textBox, KeyEventArgs e);
        void InputTab(TextEditor textBox, KeyEventArgs e);
        void InputEnter(TextEditor textBox, KeyEventArgs e);
        void InputMinus(TextEditor textBox, KeyEventArgs e);
        void InputPlus(TextEditor textBox, KeyEventArgs e);
        void InputEquality(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     }
        /// </summary>
        /// <returns></returns>
        void InputCloseBrace(TextEditor textBox, KeyEventArgs e);
        void InputPeriod(TextEditor textBox, KeyEventArgs e);
        void InputGreaterThan(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     :
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputColon(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     ;
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputSemicolon(TextEditor textBox, KeyEventArgs e);
        void InputMultiply(TextEditor textBox, KeyEventArgs e);
        void InputDivide(TextEditor textBox, KeyEventArgs e);
        void InputQuestion(TextEditor textBox, KeyEventArgs e);
        void InputPercent(TextEditor textBox, KeyEventArgs e);
        void InputAnd(TextEditor textBox, KeyEventArgs e);
        void InputBackslash(TextEditor textBox, KeyEventArgs e);
        void InputOr(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     ^
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputXor(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     !
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputExclamation(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     ~
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputReverse(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     `
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputOem3(TextEditor textBox, KeyEventArgs e);
        void InputLessThan(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     ,
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="e"></param>
        void InputComma(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     ]
        /// </summary>
        /// <returns></returns>
        void InputCloseBracket(TextEditor textBox, KeyEventArgs e);
        /// <summary>
        ///     )
        /// </summary>
        /// <returns></returns>
        void InputCloseParenthese(TextEditor textBox, KeyEventArgs e);
        void InputAt(TextEditor textBox, KeyEventArgs e);
        void InputSharp(TextEditor textBox, KeyEventArgs e);
        void InputDollar(TextEditor textBox, KeyEventArgs e);
        void InputSpace(TextEditor textBox, KeyEventArgs e);
        void Copy(TextEditor textBox, KeyEventArgs e);
        void Cut(TextEditor textBox, KeyEventArgs e);
        void Redo(TextEditor textBox);
    }
}