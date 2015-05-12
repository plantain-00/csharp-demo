using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var trie = new Trie();
            trie.Insert("abc");
            trie.Insert("ab");
            trie.Insert("bd");
            trie.Insert("dda");
        }

        [TestMethod]
        public void SearchTest()
        {
            var trie = new Trie();
            trie.Insert("abc");
            trie.Insert("ab");
            trie.Insert("bd");
            trie.Insert("dda");

            Assert.IsTrue(trie.Search("abc"));
            Assert.IsTrue(trie.Search("ab"));
            Assert.IsTrue(trie.Search("bd"));
            Assert.IsTrue(trie.Search("dda"));
            Assert.IsFalse(trie.Search("abdc"));
            Assert.IsFalse(trie.Search("aeb"));
            Assert.IsFalse(trie.Search("bfd"));
            Assert.IsFalse(trie.Search("da"));
        }

        [TestMethod]
        public void StartsWithTest()
        {
            var trie = new Trie();
            trie.Insert("abc");
            trie.Insert("ab");
            trie.Insert("bd");
            trie.Insert("dda");

            Assert.IsTrue(trie.StartsWith("abc"));
            Assert.IsTrue(trie.StartsWith("ab"));
            Assert.IsTrue(trie.StartsWith("bd"));
            Assert.IsTrue(trie.StartsWith("dda"));
            Assert.IsFalse(trie.StartsWith("abdc"));
            Assert.IsFalse(trie.StartsWith("aeb"));
            Assert.IsFalse(trie.StartsWith("bfd"));
            Assert.IsFalse(trie.StartsWith("da"));

            Assert.IsTrue(trie.StartsWith("b"));
            Assert.IsTrue(trie.StartsWith("dd"));
        }
    }
}