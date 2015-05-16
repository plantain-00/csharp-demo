namespace LeetCode.Solutions.Argorithms
{
    public class _19_RemoveNthNodeFromEndofList
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var current = head;
            for (var i = 0; i < n; i++)
            {
                current = current.next;
            }

            var tmp = head;

            if (current == null)
            {
                return head.next;
            }

            current = current.next;

            while (current != null)
            {
                current = current.next;
                tmp = tmp.next;
            }

            tmp.next = tmp.next.next;

            return head;
        }
    }
}