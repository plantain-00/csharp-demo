using System;
using System.Collections.Generic;
using System.Web.Mvc;

using TokenBasedWebsiteDemo.Services;

namespace TokenBasedWebsiteDemo.Controllers
{
    public class BaseController : Controller
    {
        private bool _disposed;
        private Dictionary<Type, BaseService> _factory;

        public TGenerator Get<TGenerator>() where TGenerator : BaseService, new()
        {
            if (_factory == null)
            {
                _factory = new Dictionary<Type, BaseService>();
            }
            var type = typeof (TGenerator);
            if (!_factory.ContainsKey(type))
            {
                _factory.Add(type, new TGenerator());
            }
            return _factory[type] as TGenerator;
        }

        public bool CanGet<TGenerator>() where TGenerator : class, new()
        {
            if (_factory == null)
            {
                return false;
            }
            var type = typeof (TGenerator);
            return _factory.ContainsKey(type);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        ~BaseController()
        {
            Dispose(false);
        }

        protected new virtual void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_factory != null)
                {
                    foreach (var t in _factory)
                    {
                        t.Value.Dispose();
                    }
                }
            }
            _disposed = true;
        }
    }
}