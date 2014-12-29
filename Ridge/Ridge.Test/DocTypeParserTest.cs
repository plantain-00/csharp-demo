using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class DocTypeParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("<!DOCTYPE html>");
            var parser = new DocTypeParser(html, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.DocType.Name == "!DOCTYPE");
            Assert.IsTrue(parser.DocType.Declaration == " html");
        }

        [TestMethod]
        public void HTML_401_Strict()
        {
            var html = new LexicalAnalysis().Analyse("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">");
            var parser = new DocTypeParser(html, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.DocType.Name == "!DOCTYPE");
            Assert.IsTrue(parser.DocType.Declaration == " HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\"");
        }
    }
}