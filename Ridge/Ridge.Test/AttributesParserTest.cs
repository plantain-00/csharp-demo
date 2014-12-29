using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class AttributesParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("id=\"abc\" name=\"def\"");
            var parser = new AttributesParser(html, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attributes.Count == 2);
            Assert.IsTrue(parser.Attributes[0].Name == "id");
            Assert.IsTrue(parser.Attributes[0].Value == "abc");
            Assert.IsTrue(parser.Attributes[1].Name == "name");
            Assert.IsTrue(parser.Attributes[1].Value == "def");
        }

        [TestMethod]
        public void End_By_Slash()
        {
            var html = new LexicalAnalysis().Analyse("id=\"abc\" name=\"def\" />");
            var parser = new AttributesParser(html, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attributes.Count == 2);
            Assert.IsTrue(parser.Attributes[0].Name == "id");
            Assert.IsTrue(parser.Attributes[0].Value == "abc");
            Assert.IsTrue(parser.Attributes[1].Name == "name");
            Assert.IsTrue(parser.Attributes[1].Value == "def");
        }

        [TestMethod]
        public void End_By_LargerThan()
        {
            var html = new LexicalAnalysis().Analyse("id=\"abc\" name=\"def\" >");
            var parser = new AttributesParser(html, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attributes.Count == 2);
            Assert.IsTrue(parser.Attributes[0].Name == "id");
            Assert.IsTrue(parser.Attributes[0].Value == "abc");
            Assert.IsTrue(parser.Attributes[1].Name == "name");
            Assert.IsTrue(parser.Attributes[1].Value == "def");
        }
    }
}