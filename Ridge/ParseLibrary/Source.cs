using System;

namespace ParseLibrary
{
    public class Source
    {
        private readonly string _s;

        public Source(string s, int index = 0)
        {
            _s = s;
            Index = index;
        }

        public int Index { get; set; }

        public bool IsTail { get; private set; }

        public string Context
        {
            get
            {
                var startIndex = Math.Max(0, Index - 5);
                var endIndex = Math.Min(_s.Length - 1, Index + 5);
                return _s.Substring(startIndex, endIndex - startIndex + 1);
            }
        }

        private void MoveUntil(Func<char, bool> condition)
        {
            for (; Index < _s.Length; Index++)
            {
                if (condition(_s[Index]))
                {
                    return;
                }
            }
            IsTail = true;
        }

        public string TakeUntil(Func<char, bool> condition)
        {
            var startIndex = Index;
            for (; Index < _s.Length; Index++)
            {
                if (condition(_s[Index]))
                {
                    return _s.Substring(startIndex, Index - startIndex);
                }
            }
            IsTail = true;
            return _s.Substring(startIndex, Index - startIndex);
        }

        public void SkipIt()
        {
            Skip(1);
        }

        public void Skip(int step)
        {
            if (Index + step < _s.Length)
            {
                Index += step;
            }
            else
            {
                IsTail = true;
            }
        }

        public void Skip(string s)
        {
            Skip(s.Length);
        }

        public bool Is(char c)
        {
            return c == _s[Index];
        }

        public bool IsNot(char c)
        {
            return c != _s[Index];
        }

        public bool Is(string s, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return _s.Substring(Index, s.Length).Is(s, true);
            }
            return _s.Substring(Index, s.Length) == s;
        }

        public bool IsNot(string s, bool ignoreCase = false)
        {
            return !Is(s, ignoreCase);
        }

        public void SkipWhiteSpace()
        {
            MoveUntil(c => !char.IsWhiteSpace(c));
        }

        public void Expect(char c)
        {
            if (IsNot(c))
            {
                throw new ParseException(this);
            }
        }

        public void Expect(string s, bool ignoreCase = false)
        {
            if (IsNot(s, ignoreCase))
            {
                throw new ParseException(this);
            }
        }

        public void ExpectNot(char c)
        {
            if (Is(c))
            {
                throw new ParseException(this);
            }
        }

        public void ExpectNot(string s, bool ignoreCase = false)
        {
            if (Is(s, ignoreCase))
            {
                throw new ParseException(this);
            }
        }

        public void ExpectEnd()
        {
            if (!IsTail)
            {
                throw new ParseException(this);
            }
        }
    }
}