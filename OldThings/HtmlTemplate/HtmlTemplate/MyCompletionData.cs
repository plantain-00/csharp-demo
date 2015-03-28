using System;
using System.Windows.Media;

using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace HtmlTemplate
{
    /// <summary>
    ///     Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down.
    /// </summary>
    public class MyCompletionData : ICompletionData
    {
        public MyCompletionData(string text)
        {
            Text = text;
        }
        public ImageSource Image
        {
            get
            {
                return null;
            }
        }
        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        public object Content
        {
            get
            {
                return Text;
            }
        }
        public object Description
        {
            get
            {
                return "Description for " + Text;
            }
        }
        public double Priority
        {
            get
            {
                return 0;
            }
        }
        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, Text);
        }
    }
}