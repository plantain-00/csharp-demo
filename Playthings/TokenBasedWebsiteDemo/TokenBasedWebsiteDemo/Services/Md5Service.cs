using System;
using System.Security.Cryptography;
using System.Text;

namespace TokenBasedWebsiteDemo.Services
{
    public class Md5Service : BaseService
    {
        private static readonly MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

        public string Md5(string password)
        {
            return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
        }
    }
}