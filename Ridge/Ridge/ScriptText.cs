using ParseLibrary;

namespace Ridge
{
    public class ScriptText : Node
    {
        public string Text { get; set; }

        public override string ToString(Formatting formatting, int spaceNumber = 4)
        {
            if (formatting == Formatting.None)
            {
                return Text.Trim(' ', '\n', '\r', '\t');
            }
            return string.Format("{0}{1}\n", new string(' ', Depth * spaceNumber), Text.Trim(' ', '\n', '\r', '\t'));
        }

        internal static ScriptText Create(Source source, Node parent, int depth)
        {
            var result = new ScriptText
                         {
                             Depth = depth,
                             Text = source.TakeUntil("</script>").TrimEnd(),
                             Parent = parent
                         };
            return result;
        }
    }
}