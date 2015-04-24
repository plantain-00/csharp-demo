namespace LeetCode.Solutions.Argorithms
{
    public class _203_RemoveLinkedListElements
    {
        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
            {
                return null;
            }
            head.next = RemoveElements(head.next, val);
            return head.val == val ? head.next : head;
        }
    }
}