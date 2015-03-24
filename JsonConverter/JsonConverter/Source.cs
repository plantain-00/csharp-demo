using System;
using System.Linq;

namespace JsonConverter
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

        public bool IsTail
        {
            get
            {
                return Index == _s.Length - 1;
            }
        }

        public int Length
        {
            get
            {
                return _s.Length;
            }
        }

        private char Next(int i = 0)
        {
            return _s[Index + i];
        }

        public string Substring(int startIndex, int length)
        {
            return _s.Substring(startIndex, length);
        }

        public void MoveUntil(Func<char, bool> condition)
        {
            for (; Index < _s.Length; Index++)
            {
                if (condition(_s[Index]))
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

        public void SkipWhiteSpace()
        {
            MoveUntil(c => !char.IsWhiteSpace(c));
        }
    }
}