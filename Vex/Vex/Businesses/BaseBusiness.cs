using System;

using Vex.Services;

namespace Vex.Businesses
{
    public class BaseBusiness : IDisposable
    {
        private AccountService _accountService;
        private BaseService _baseService;
        private bool _disposed;

        public AccountService Account
        {
            get
            {
                return _accountService ?? (_accountService = new AccountService());
            }
        }


        public BaseService Base
        {
            get
            {
                return _baseService ?? (_baseService = new BaseService());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        ~BaseBusiness()
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
                if (_accountService != null)
                {
                    _accountService.Dispose();
                }
                if (_baseService != null)
                {
                    _baseService.Dispose();
                }
            }
            _disposed = true;
        }
    }
}