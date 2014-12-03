using System;
using System.Web.Mvc;

using Ioc.Net.Model.Businesses;

namespace Ioc.Net.Model.Controllers
{
    public class BaseController : Controller
    {
        private bool _disposed;
        private IUserBusiness _userBusiness;
        public virtual IUserBusiness UserBusiness
        {
            get
            {
                return _userBusiness ?? (_userBusiness = Ioc.CreateInstance<IUserBusiness>());
            }
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
                if (_userBusiness != null)
                {
                    _userBusiness.Dispose();
                }
            }
            _disposed = true;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // handle the exception
            filterContext.ExceptionHandled = true;
        }
    }
}