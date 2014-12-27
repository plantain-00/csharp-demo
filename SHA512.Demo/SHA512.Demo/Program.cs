using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SHA512.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sha256 = SHA256.Create();
            var result = sha256.ComputeHash("Hello Wolrd!".Select(c => (byte) c).ToArray());
            var b = HttpServerUtility.UrlTokenEncode(result);
            var a = HttpServerUtility.UrlTokenDecode(b);
            Console.Read();
        }
    }
}