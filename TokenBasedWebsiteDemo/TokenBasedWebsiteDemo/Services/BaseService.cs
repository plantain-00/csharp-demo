using System;
using System.Web;
using System.Web.Caching;

namespace TokenBasedWebsiteDemo.Services
{
    public class BaseService
    {
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
    }
}