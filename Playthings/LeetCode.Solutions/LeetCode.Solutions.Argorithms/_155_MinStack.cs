using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _155_MinStack
    {
        private readonly Stack<int> _minStack;
        private readonly Stack<int> _stack;

        public _155_MinStack()
        {
            _stack = new Stack<int>();
            _minStack = new Stack<int>();
        }

        public void Push(int x)
        {
            _stack.Push(x);
            if (_minStack.Count == 0)
            {
                _minStack.Push(x);
            }
            else
            {
                var min = _minStack.Peek();
                if (x < min)
                {
                    _minStack.Push(x);
                }
                else
                {
                    _minStack.Push(min);
                }
            }
        }

        public void Pop()
        {
            _stack.Pop();
            _minStack.Pop();
        }

        public int Top()
        {
            return _stack.Peek();
        }

        public int GetMin()
        {
            return _minStack.Peek();
        }
    }
}