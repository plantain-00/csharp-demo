using System.Text;

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

            var source = new Source(html);

            int startIndex;

            while (true)
            {
                source.MoveUntil(c => c == '<');
                source.MoveForward();

                source.MoveUntil(c => c != ' ');

                if (source.IsNot("title", 0, true))
                {
                    continue;
                }
                source.MoveForward("title".Length);

                source.MoveUntil(c => c != ' ');

                if (source.IsNot('>'))
                {
                    continue;
                }

                source.MoveForward();
                startIndex = source.Index;
                break;
            }

            int length;

            while (true)
            {
                source.MoveUntil(c => c == '<');
                var endIndex = source.Index;
                source.MoveForward();

                source.MoveUntil(c => c != ' ');

                if (source.IsNot('/'))
                {
                    continue;
                }
                source.MoveForward();

                source.MoveUntil(c => c != ' ');
                if (source.IsNot("title", 0, true))
                {
                    continue;
                }
                source.MoveForward("title".Length);

                source.MoveUntil(c => c != ' ');

                if (source.IsNot('>'))
                {
                    continue;
                }

                source.MoveForward();
                length = endIndex - startIndex;
                break;
            }

            Title = html.Substring(startIndex, length);
        }

        public string Title { get; private set; }
    }
}