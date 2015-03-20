using System;
using System.Net;

namespace ReadableUrl
{
    public class XWebClient : WebClient
    {
        public XWebClient()
        {
            Cookies = new CookieContainer();
        }
        public CookieContainer Cookies { get; private set; }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            if (request.CookieContainer == null)
            {
                request.CookieContainer = Cookies;
            }
            return request;
        }
    }
}