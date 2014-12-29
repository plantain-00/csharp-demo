using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class CommentParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("<!--abc-->");
            var parser = new CommentParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Comment.Text == "!--abc--");
        }
    }
}