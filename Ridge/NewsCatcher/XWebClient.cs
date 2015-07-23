using System;
using System.Net;

namespace NewsCatcher
{
    public class XWebClient : WebClient
    {
        public XWebClient()
        {
            Cookies = new CookieContainer();
        }
        public CookieContainer Cookies { get; }
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