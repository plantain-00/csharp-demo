using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class AttributeParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("id=\"abc\"");
            var parser = new AttributeParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attribute.Name == "id");
            Assert.IsTrue(parser.Attribute.Value == "abc");
        }

        [TestMethod]
        public void With_Spaces()
        {
            var html = new LexicalAnalysis().Analyse("id = \"abc\"");
            var parser = new AttributeParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attribute.Name == "id");
            Assert.IsTrue(parser.Attribute.Value == "abc");
        }

        [TestMethod]
        public void No_Value_By_Space()
        {
            var html = new LexicalAnalysis().Analyse("id ");
            var parser = new AttributeParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Attribute.Name == "id");
            Assert.IsTrue(parser.Attribute.Value == null);
        }

        [TestMethod]
        public void No_Value_By_Slash()
        {
            var html = new LexicalAnalysis().Analyse("id/>");
            var parser = new AttributeParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count - 2);
            Assert.IsTrue(parser.Attribute.Name == "id");
            Assert.IsTrue(parser.Attribute.Value == null);
        }

        [TestMethod]
        public void No_Value_By_LargerThan()
        {
            var html = new LexicalAnalysis().Analyse("id>");
            var parser = new AttributeParser(html, 0, html.Count);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count - 1);
            Assert.IsTrue(parser.Attribute.Name == "id");
            Assert.IsTrue(parser.Attribute.Value == null);
        }
    }
}