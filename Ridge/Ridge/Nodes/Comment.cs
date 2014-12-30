namespace Ridge.Nodes
{
    public class Comment : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return string.Format("<{0}>", Text);
            }
            return string.Format("{0}<{1}>\n", new string(CHAR.SPACE, Depth * spaceNumber), Text);
        }

        public override string ToString()
        {
            return ToString(Formatting.Indented);
        }
    }
}