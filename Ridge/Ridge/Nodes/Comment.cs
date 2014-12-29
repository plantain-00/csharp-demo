namespace Ridge.Nodes
{
    public class Comment : Node
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0}<{1}>\n", new string(CHAR.SPACE, Depth * 4), Text);
        }
    }
}