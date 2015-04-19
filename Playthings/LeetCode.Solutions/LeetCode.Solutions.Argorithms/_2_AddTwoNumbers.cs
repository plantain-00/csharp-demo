namespace LeetCode.Solutions.Argorithms
{
    public class _2_AddTwoNumbers
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var head = new ListNode(0);
            var current = head;
            var carry = false;

            while (l1 != null
                   || l2 != null)
            {
                int sum;

                if (l1 == null)
                {
                    sum = l2.val;
                    l2 = l2.next;
                }
                else if (l2 == null)
                {
                    sum = l1.val;
                    l1 = l1.next;
                }
                else
                {
                    sum = l1.val + l2.val;
                    l1 = l1.next;
                    l2 = l2.next;
                }

                if (carry)
                {
                    sum++;
                }

                if (sum >= 10)
                {
                    sum = sum - 10;
                    carry = true;
                }
                else
                {
                    carry = false;
                }

                current.next = new ListNode(sum);
                current = current.next;
            }

            if (carry)
            {
                current.next = new ListNode(1);
            }

            return head.next;
        }
    }
}