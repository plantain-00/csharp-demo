using Microsoft.VisualStudio.TestTools.UnitTesting;

using Ridge.Nodes;
using Ridge.Parsers;

namespace Ridge.Test
{
    [TestClass]
    public class TagParserTest
    {
        [TestMethod]
        public void Normal()
        {
            var html = new LexicalAnalysis().Analyse("<div id=\"abc\"><span>def</span></div>");
            var parser = new TagParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Tag.HasSlash == false);
            Assert.IsTrue(parser.Tag.Name == "div");
            Assert.IsTrue(parser.Tag.Depth == 0);
            Assert.IsTrue(parser.Tag.Attributes.Count == 1);
            Assert.IsTrue(parser.Tag.Attributes[0].Name == "id");
            Assert.IsTrue(parser.Tag.Attributes[0].Value == "abc");
            Assert.IsTrue(parser.Tag.Children.Count == 1);
            Assert.IsTrue(parser.Tag.Children[0].Depth == 1);
            Assert.IsTrue(parser.Tag.Children[0] is Tag);
            var tag = parser.Tag.Children[0] as Tag;
            Assert.IsTrue(tag.HasSlash == false);
            Assert.IsTrue(tag.Attributes == null);
            Assert.IsTrue(tag.Name == "span");
            Assert.IsTrue(tag.Children.Count == 1);
            Assert.IsTrue(tag.Children[0].Depth == 2);
            Assert.IsTrue(tag.Children[0] is PlainText);
            Assert.IsTrue((tag.Children[0] as PlainText).Text == "def");
        }

        [TestMethod]
        public void Has_Slash()
        {
            var html = new LexicalAnalysis().Analyse("<input type=\"text\"/>");
            var parser = new TagParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Tag.HasSlash);
            Assert.IsTrue(parser.Tag.Children == null);
        }

        [TestMethod]
        public void Same_Tag_Name()
        {
            var html = new LexicalAnalysis().Analyse("<div><div>def</div></div>");
            var parser = new TagParser(html, 0, 0);
            parser.Parse();
            Assert.IsTrue(parser.Index == html.Count);
            Assert.IsTrue(parser.Tag.Children.Count == 1);
            Assert.IsTrue((parser.Tag.Children[0] as Tag).Children.Count == 1);
        }
    }
}