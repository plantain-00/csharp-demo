namespace LeetCode.Solutions.Argorithms
{
    public class _82_RemoveDuplicatesfromSortedListII
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            if (head.next != null
                && head.val == head.next.val)
            {
                while (head.next != null
                       && head.val == head.next.val)
                {
                    head = head.next;
                }
                return DeleteDuplicates(head.next);
            }
            head.next = DeleteDuplicates(head.next);
            return head;
        }
    }
}