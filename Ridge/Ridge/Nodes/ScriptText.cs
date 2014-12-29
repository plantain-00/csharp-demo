namespace Ridge.Nodes
{
    public class ScriptText : Node
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}