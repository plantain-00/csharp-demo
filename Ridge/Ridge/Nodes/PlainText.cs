﻿namespace Ridge.Nodes
{
    public class PlainText : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return Text.Trim(' ', '\n', '\r', '\t');
            }
            return string.Format("{0}{1}\n", new string(CHAR.SPACE, Depth * spaceNumber), Text.Trim(' ', '\n', '\r', '\t'));
        }

        public override string ToString()
        {
            return ToString(Formatting.Indented);
        }
    }
}