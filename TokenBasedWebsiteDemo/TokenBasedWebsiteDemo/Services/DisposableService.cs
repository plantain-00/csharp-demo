using System;

using TokenBasedWebsiteDemo.DbModels;

namespace TokenBasedWebsiteDemo.Services
{
    public class DisposableService : BaseService, IDisposable
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

        ~DisposableService()
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