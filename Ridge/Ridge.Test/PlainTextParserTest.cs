using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class PlainTextParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("abc");
            var parser = new PlainTextParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.PlainText.Text == "abc");
        }

        [TestMethod]
        public void With_Spaces()
        {
            var html = new LexicalAnalysis().Analyse("  a  b  c  ");
            var parser = new PlainTextParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.PlainText.Text == "  a  b  c  ");
        }

        [TestMethod]
        public void With_Single_Line_Comment()
        {
            var html = new LexicalAnalysis().Analyse("abc//<def/>\n");
            var parser = new PlainTextParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.PlainText.Text == "abc//<def/>\n");
        }
    }
}