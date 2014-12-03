using System.IO;

namespace Ioc.Net.Model.Services
{
    public class HtmlService : BaseService, IHtmlService
    {
        public void Save(string fileName, string html)
        {
            var path = GetPath("/Htmls/" + fileName);
            File.WriteAllText(path, html);
        }

        public string Read(string fileName)
        {
            var path = GetPath("/Htmls/" + fileName);
            return File.ReadAllText(path);
        }

        public void Delete(string fileName)
        {
            var path = GetPath("/Htmls/" + fileName);
            File.Delete(path);
        }
    }
}