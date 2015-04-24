using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _203_RemoveLinkedListElementsTests
    {
        [TestMethod]
        public void RemoveElementsTest()
        {
            var list = new ListNode(1)
                       {
                           next = new ListNode(2)
                                  {
                                      next = new ListNode(6)
                                             {
                                                 next = new ListNode(3)
                                                        {
                                                            next = new ListNode(4)
                                                                   {
                                                                       next = new ListNode(5)
                                                                              {
                                                                                  next = new ListNode(6)
                                                                              }
                                                                   }
                                                        }
                                             }
                                  }
                       };
            var result = new _203_RemoveLinkedListElements().RemoveElements(list, 6);
            Assert.IsTrue(result.val == 1);
            Assert.IsTrue(result.next.val == 2);
            Assert.IsTrue(result.next.next.val == 3);
            Assert.IsTrue(result.next.next.next.val == 4);
            Assert.IsTrue(result.next.next.next.next.val == 5);
            Assert.IsTrue(result.next.next.next.next.next == null);
        }
    }
}