using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _3_LongestSubstringWithoutRepeatingCharactersTests
    {
        [TestMethod]
        public void LengthOfLongestSubstringTest()
        {
            var a = new _3_LongestSubstringWithoutRepeatingCharacters();
            Assert.IsTrue(a.LengthOfLongestSubstring("abcabcbb") == 3);
            Assert.IsTrue(a.LengthOfLongestSubstring("bbbbb") == 1);
        }
    }
}