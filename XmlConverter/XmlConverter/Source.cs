using System;

namespace XmlConverter
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

        public int Length
        {
            get
            {
                return _s.Length;
            }
        }

        public string Context
        {
            get
            {
                var startIndex = Math.Max(0, Index - 5);
                var endIndex = Math.Min(Length - 1, Index + 5);
                return Substring(startIndex, endIndex - startIndex + 1);
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
            IsTail = true;
        }

        public string TakeUntil(Func<char, bool> condition)
        {
            var startIndex = Index;
            for (; Index < _s.Length; Index++)
            {
                if (condition(_s[Index]))
                {
                    return Substring(startIndex, Index - startIndex);
                }
            }
            IsTail = true;
            return Substring(startIndex, Index - startIndex);
        }

        public void MoveForward(int step = 1)
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

        public bool Is(char c, bool ignoreCase = false)
        {
            var next = Next();
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

        public bool IsNot(char c, bool ignoreCase = false)
        {
            return !Is(c, ignoreCase);
        }

        public bool Is(string s, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                return String.Equals(_s.Substring(Index, s.Length), s, StringComparison.CurrentCultureIgnoreCase);
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

        public void Expect(char c, bool ignoreCase = false)
        {
            if (IsNot(c))
            {
                throw new ParseException(this);
            }
        }

        public void Expect(string s, bool ignoreCase = false)
        {
            if (IsNot(s))
            {
                throw new ParseException(this);
            }
        }
        public void ExpectNot(char c, bool ignoreCase = false)
        {
            if (Is(c))
            {
                throw new ParseException(this);
            }
        }

        public void ExpectNot(string s, bool ignoreCase = false)
        {
            if (Is(s))
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