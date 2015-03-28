using System;
using System.Web.Mvc;

using Vex.Services;

namespace Vex.Controllers
{
    public class BaseController : Controller
    {
        private bool _disposed;
        public Lazy<MailService> Mail = new Lazy<MailService>();
        public Lazy<BaseService> Base = new Lazy<BaseService>();
        public Lazy<AccountService> Account = new Lazy<AccountService>();

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
                if (Base.IsValueCreated)
                {
                    Base.Value.Dispose();
                }
                if (Mail.IsValueCreated)
                {
                    Mail.Value.Dispose();
                }
                if (Account.IsValueCreated)
                {
                    Account.Value.Dispose();
                }
            }
            _disposed = true;
        }
    }
}