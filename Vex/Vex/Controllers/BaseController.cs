using System;
using System.Web.Mvc;

using Vex.Businesses;

namespace Vex.Controllers
{
    public class BaseController : Controller
    {
        private BaseBusiness _baseBusiness;
        private bool _disposed;
        private MailBusiness _mailBusiness;
        public MailBusiness Mail
        {
            get
            {
                return _mailBusiness ?? (_mailBusiness = new MailBusiness());
            }
        }
        public virtual BaseBusiness Base
        {
            get
            {
                return _baseBusiness ?? (_baseBusiness = new BaseBusiness());
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
                if (_baseBusiness != null)
                {
                    _baseBusiness.Dispose();
                }
                if (_mailBusiness != null)
                {
                    _mailBusiness.Dispose();
                }
            }
            _disposed = true;
        }
    }
}