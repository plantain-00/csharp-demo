﻿using System.Text;

namespace ReadableUrl
{
    public class Client
    {
        public Client(string url) : this(url, Encoding.UTF8)
        {
        }

        public Client(string url, Encoding encoding)
        {
            var html = new XWebClient
                       {
                           Encoding = encoding
                       }.DownloadString(url);
            const int FLAG = -1;
            var startIndex = FLAG;
            for (var i = 0; i + 7 < html.Length; i++)
            {
                if (html[i] != '<')
                {
                    continue;
                }
                while (html[i + 1] == ' ')
                {
                    i++;
                }
                if (!Is(html[i + 1], 't')
                    || !Is(html[i + 2], 'i')
                    || !Is(html[i + 3], 't')
                    || !Is(html[i + 4], 'l')
                    || !Is(html[i + 5], 'e'))
                {
                    continue;
                }
                while (html[i + 6] == ' ')
                {
                    i++;
                }
                if (html[i + 6] == '>')
                {
                    startIndex = i + 7;
                }
            }

            if (startIndex == FLAG)
            {
                Title = string.Empty;
                return;
            }

            var length = 0;
            for (var i = startIndex; i + 7 < html.Length; i++)
            {
                length++;
                if (html[i] != '<')
                {
                    continue;
                }
                while (html[i + 1] == ' ')
                {
                    i++;
                }
                if (html[i + 1] != '/')
                {
                    continue;
                }
                while (html[i + 2] == ' ')
                {
                    i++;
                }
                if (!Is(html[i + 2], 't')
                    || !Is(html[i + 3], 'i')
                    || !Is(html[i + 4], 't')
                    || !Is(html[i + 5], 'l')
                    || !Is(html[i + 6], 'e'))
                {
                    continue;
                }
                while (html[i + 7] == ' ')
                {
                    i++;
                }
                if (html[i + 7] == '>')
                {
                    break;
                }
            }

            if (length > 0)
            {
                length--;
            }

            Title = html.Substring(startIndex, length);
        }

        public string Title { get; private set; }
        public string Link { get; set; }

        private static bool Is(char c1, char c2)
        {
            if (char.IsUpper(c2))
            {
                return c1 == c2 || c1 == char.ToLower(c2);
            }
            if (char.IsLower(c2))
            {
                return c1 == c2 || c1 == char.ToUpper(c2);
            }
            return c1 == c2;
        }
    }
}