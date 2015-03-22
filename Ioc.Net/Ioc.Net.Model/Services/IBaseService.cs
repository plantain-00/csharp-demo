using System;

using ExcelFile.net;

using Ioc.Net.Model.Models;

namespace Ioc.Net.Model.Services
{
    public interface IBaseService : IDisposable
    {
        Entities Entities { get; }
        User CurrentUser { get; }
        void Close();
        string Sql { get; }

        T GetCache<T>(string key);
        bool HasCache<T>(string key);
        bool HasCache(string key);
        void SetCache(string key, object value, TimeSpan timeSpan);
        void ClearCache(string key);

        T GetSession<T>(string key);
        bool HasSession<T>(string key);
        bool HasSession(string key);
        void SetSession(string key, object value);
        void RemoveSession(string key);

        string GetCookie(string key);
        bool HasCookie(string key);
        void SetCookie(string key, string value);

        void ExportExcel(ExcelEditor excel, string name);
        void SaveUploadedFile();
        string GetPath(string filePath);

        string QueryString(string key);
        string Form(string key);
    }
}
