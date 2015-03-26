using XmlConverter.Nodes;

namespace XmlConverter.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //const string XML = " alt='Foligno Madonna, by Raphael'";
            const string XML = " <!--no need to escape <code> & such in comments--> ";
            var document = Document.Create(XML);
            var description = document.ToString(Formatting.Indented);
        }
    }
}