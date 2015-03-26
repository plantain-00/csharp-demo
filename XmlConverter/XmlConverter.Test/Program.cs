using XmlConverter.Nodes;

namespace XmlConverter.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string XML = " alt='Foligno Madonna, by Raphael'";
            var document = Document.Create(XML);
            var description = document.ToString(Formatting.Indented);
        }
    }
}