using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetCode.Solutions.Argorithms.Tests
{
    [TestClass]
    public class _160_IntersectionOfTwoLinkedListsTests
    {
        [TestMethod]
        public void GetIntersectionNodeTest()
        {
            var c = new ListNode(31)
                    {
                        next = new ListNode(32)
                               {
                                   next = new ListNode(33)
                               }
                    };
            var a = new ListNode(11)
                    {
                        next = new ListNode(12)
                               {
                                   next = c
                               }
                    };
            var b = new ListNode(21)
                    {
                        next = new ListNode(22)
                               {
                                   next = new ListNode(23)
                                          {
                                              next = c
                                          }
                               }
                    };
            Assert.IsTrue(new _160_IntersectionOfTwoLinkedLists().GetIntersectionNode(a, b) == c);
        }
    }
}