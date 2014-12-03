namespace Ioc.Net.Model.Services
{
    public interface IHtmlService : IBaseService
    {
        void Save(string fileName, string html);
        string Read(string fileName);
        void Delete(string fileName);
    }
}