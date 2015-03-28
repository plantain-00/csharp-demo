using System;

namespace ReadableUrl
{
    public class Source
    {
        private readonly string _s;

        public Source(string s, int index = 0)
        {
            _s = s;
            Index = index;
        }

        public int Index { get; private set; }

        private char Next(int i = 0)
        {
            return _s[Index + i];
        }

        public void MoveUntil(Func<char, bool> condition)
        {
            for (var i = Index; i < _s.Length; i++)
            {
                if (condition(_s[i]))
                {
                    return;
                }
            }
            throw new Exception();
        }

        public void MoveForward(int step = 1)
        {
            if (Index + step >= _s.Length)
            {
                throw new Exception();
            }
            Index += step;
        }

        public bool Is(char c, int i = 0, bool ignoreCase = false)
        {
            var next = Next(i);
            if (!ignoreCase)
            {
                return c == next;
            }
            if (char.IsUpper(next))
            {
                return c == next || c == char.ToLower(next);
            }
            if (char.IsLower(next))
            {
                return c == next || c == char.ToUpper(next);
            }
            return c == next;
        }

        public bool IsNot(char c, int i = 0, bool ignoreCase = false)
        {
            return !Is(c, i, ignoreCase);
        }

        public bool Is(string s, int i = 0, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return String.Equals(_s.Substring(Index + i, s.Length), s, StringComparison.CurrentCultureIgnoreCase);
            }
            return _s.Substring(Index + i, s.Length) == s;
        }

        public bool IsNot(string s, int i = 0, bool ignoreCase = false)
        {
            return !Is(s, i, ignoreCase);
        }
    }
}