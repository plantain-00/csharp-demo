using System;

using RetrievePassword;

namespace TokenBasedWebsiteDemo.Services
{
    public class TokenService : BaseService
    {
        public string Generate(string password, int userId)
        {
            var retriever = new Retriever();
            string seconds;
            var result = retriever.Generate(password, out seconds);
            return result + 'G' + seconds + 'G' + userId.ToString("x");
        }

        public int GetUserId(string token)
        {
            var tmp = token.Split('G');
            return Convert.ToInt32(tmp[2], 16);
        }
    }
}