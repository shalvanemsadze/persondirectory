using System;
using System.Linq;
using System.Collections.Generic;
using PersonDirectory.Data;

namespace PersonDirectory.Repository
{
    public class UnitOfWork : IDisposable
    {
        private PersonDirectoryContext Context = null;
        private Dictionary<Type, object> repositories = null;

        public UnitOfWork(PersonDirectoryContext context)
        {
            Context = context;
            repositories = new Dictionary<Type, object>(); 
        }

        public TIRepository Repository<TIRepository,TRepository>() where TIRepository : class  
        {
            if (repositories.Keys.Contains(typeof(TIRepository)))
                return (TIRepository)repositories[typeof(TIRepository)];

            TRepository repository = (TRepository)Activator.CreateInstance(typeof(TRepository), args: Context);
            repositories.Add(typeof(TIRepository), repository);
            return (TIRepository)repositories[typeof(TIRepository)];
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
