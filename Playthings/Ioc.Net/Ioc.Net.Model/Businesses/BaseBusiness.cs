using System;

using Ioc.Net.Model.Services;

namespace Ioc.Net.Model.Businesses
{
    public class BaseBusiness : IBaseBusiness
    {
        private bool _disposed;
        private IUserService _userService;
        public IUserService UserService
        {
            get
            {
                return _userService ?? (_userService = Ioc.CreateInstance<IUserService>());
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
                if (_userService != null)
                {
                    _userService.Dispose();
                }
            }
            _disposed = true;
        }
    }
}