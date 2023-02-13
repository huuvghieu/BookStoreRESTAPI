using BookStore.Data.Models;
using BookStore.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext _dbContext;
        public UnitOfWork(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly Dictionary<Type, object> reposotories = new Dictionary<Type, object>();
        
        public IGenericRepository<T> Repository<T>() where T : class
        {
            Type type = typeof(T);
            if(!reposotories.TryGetValue(type, out object value))
            {
                var genericRepos = new GenericRepository<T>(_dbContext);
                reposotories.Add(type, genericRepos);
                return genericRepos;
            }
            return value as GenericRepository<T>;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

    }
}
