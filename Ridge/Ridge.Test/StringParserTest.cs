using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class StringParserTest
    {
        [TestMethod]
        public void Single_Quote()
        {
            var html = new LexicalAnalysis().Analyse("'abc'");
            var parser = new StringParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.String == "abc");
        }

        [TestMethod]
        public void Double_Quote()
        {
            var html = new LexicalAnalysis().Analyse("\"abc\"");
            var parser = new StringParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.String == "abc");
        }

        [TestMethod]
        public void Double_Quote_With_Spaces()
        {
            var html = new LexicalAnalysis().Analyse("\"  a  b  c  \"");
            var parser = new StringParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.String == "  a  b  c  ");
        }

        [TestMethod]
        public void Single_Quote_In_Double_Quote()
        {
            var html = new LexicalAnalysis().Analyse("\"'\"");
            var parser = new StringParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.String == "'");
        }

        [TestMethod]
        public void Double_Quote_In_Single_Quote()
        {
            var html = new LexicalAnalysis().Analyse("'\"'");
            var parser = new StringParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.String == "\"");
        }
    }
}