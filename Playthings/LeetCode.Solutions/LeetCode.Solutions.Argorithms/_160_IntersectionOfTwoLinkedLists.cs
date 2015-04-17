namespace LeetCode.Solutions.Argorithms
{
    public class _160_IntersectionOfTwoLinkedLists
    {
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            if (headA == null
                || headB == null)
            {
                return null;
            }

            var currentA = headA;
            var currentB = headB;
            ListNode tailA = null;
            ListNode tailB = null;

            while (currentA != currentB)
            {
                if (tailA != null
                    && tailB != null
                    && tailA != tailB)
                {
                    return null;
                }
                if (currentA.next == null)
                {
                    tailA = currentA;
                    currentA = headB;
                }
                else
                {
                    currentA = currentA.next;
                }

                if (currentB.next == null)
                {
                    tailB = currentB;
                    currentB = headA;
                }
                else
                {
                    currentB = currentB.next;
                }
            }

            return currentA;
        }
    }
}