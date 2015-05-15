using System.Collections.Generic;

namespace LeetCode.Solutions.Argorithms
{
    public class _20_ValidParentheses
    {
        public bool IsValid(string s)
        {
            var stack = new Stack<char>();
            foreach (var c in s)
            {
                switch (c)
                {
                    case '{':
                    case '(':
                    case '[':
                        stack.Push(c);
                        break;
                    case '}':
                        if (stack.Count == 0)
                        {
                            return false;
                        }
                        if (stack.Pop() != '{')
                        {
                            return false;
                        }
                        break;
                    case ')':
                        if (stack.Count == 0)
                        {
                            return false;
                        }
                        if (stack.Pop() != '(')
                        {
                            return false;
                        }
                        break;
                    case ']':
                        if (stack.Count == 0)
                        {
                            return false;
                        }
                        if (stack.Pop() != '[')
                        {
                            return false;
                        }
                        break;
                }
            }
            return stack.Count == 0;
        }
    }
}