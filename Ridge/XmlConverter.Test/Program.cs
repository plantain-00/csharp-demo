using ParseLibrary;

namespace XmlConverter.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string XML = " <?xml version=\"1.0\" encoding=\"UTF-8\"?>  <Greeting name=\"sdsd\" age=\"12\" >Hello, world.<people alt='Foligno Madonna, by Raphael' /> <!--no need to escape <code> & such in comments--> <as><ed>sfsf</ed></as> </Greeting> ";

            var document = Document.Create(XML);
            var description = document.ToString(Formatting.Indented);
        }
    }
}