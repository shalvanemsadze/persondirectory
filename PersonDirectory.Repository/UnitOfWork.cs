using System;
using System.Linq;
using System.Collections.Generic;
using PersonDirectory.Data;

namespace PersonDirectory.Repository
{
    public class UnitOfWork : IDisposable
    {
        private PersonDirectoryContext _context = null;
        private Dictionary<Type, object> _repositories = null;

        public UnitOfWork(PersonDirectoryContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>(); 
        }

        public TIRepository Repository<TIRepository,TRepository>() where TIRepository : class  
        {
            if (_repositories.Keys.Contains(typeof(TIRepository)))
                return (TIRepository)_repositories[typeof(TIRepository)];

            TRepository repository = (TRepository)Activator.CreateInstance(typeof(TRepository), args: _context);
            _repositories.Add(typeof(TIRepository), repository);
            return (TIRepository)_repositories[typeof(TIRepository)];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
