using System;
using System.Web;
using System.Web.Caching;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo.Services
{
    public class BaseService : IDisposable
    {
        private bool _disposed;
        private Entities _entities;
        public Entities Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = new Entities();
                    _entities.Database.Log = sql => Sql += sql;
                }
                return _entities;
            }
        }

        public string Sql { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        public T GetCache<T>(string key)
        {
            return (T) HttpContext.Current.Cache[key];
        }

        public bool HasCache<T>(string key)
        {
            return HttpContext.Current.Cache[key] != null && typeof (T) == HttpContext.Current.Cache[key].GetType();
        }

        public bool HasCache(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        public void SetCache(string key, object value, TimeSpan timeSpan)
        {
            if (HttpContext.Current.Cache[key] != null)
            {
                HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, timeSpan, CacheItemPriority.Default, null);
            }
            else
            {
                HttpContext.Current.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, timeSpan, CacheItemPriority.Default, null);
            }
        }

        public void ClearCache(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public T GetSession<T>(string key)
        {
            return (T) HttpContext.Current.Session[key];
        }

        public bool HasSession<T>(string key)
        {
            return HttpContext.Current.Session[key] != null && typeof (T) == HttpContext.Current.Session[key].GetType();
        }

        public bool HasSession(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }

        public void SetSession(string key, object value)
        {
            HttpContext.Current.Session.Add(key, value);
        }

        public void RemoveSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public string GetCookie(string key)
        {
            return HttpContext.Current.Request.Cookies[key].Value;
        }

        public bool HasCookie(string key)
        {
            return HttpContext.Current.Request.Cookies[key] != null;
        }

        public void SetCookie(string key, string value)
        {
            HttpContext.Current.Request.Cookies.Add(new HttpCookie(key, value));
        }

        public string QueryString(string key)
        {
            return HttpContext.Current.Request.QueryString[key];
        }

        public string Form(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }

        public string GetPath(string filePath)
        {
            return HttpContext.Current.Server.MapPath("~" + filePath);
        }

        ~BaseService()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_entities != null)
                {
                    _entities.Dispose();
                }
            }
            _disposed = true;
        }
    }
}