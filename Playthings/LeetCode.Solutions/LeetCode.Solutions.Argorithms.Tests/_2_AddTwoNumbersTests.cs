using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _2_AddTwoNumbersTests
    {
        [TestMethod]
        public void AddTwoNumbersTest()
        {
            var l1 = new ListNode(2)
                     {
                         next = new ListNode(4)
                                {
                                    next = new ListNode(3)
                                }
                     };
            var l2 = new ListNode(5)
                     {
                         next = new ListNode(6)
                                {
                                    next = new ListNode(4)
                                }
                     };
            var result = new _2_AddTwoNumbers().AddTwoNumbers(l1, l2);
            Assert.IsTrue(result.val == 7);
            Assert.IsTrue(result.next.val == 0);
            Assert.IsTrue(result.next.next.val == 8);
            Assert.IsTrue(result.next.next.next == null);
        }
    }
}