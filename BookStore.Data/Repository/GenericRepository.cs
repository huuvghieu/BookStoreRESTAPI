using BookStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreContext _dbContext;
        private static DbSet<T> _dbSet;
        public GenericRepository(BookStoreContext dbContext)
        {
            _dbContext =  dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public EntityEntry<T> Delete(T entity)
        {
            return _dbContext.Remove(entity);
        }

        public IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public T Find(Func<T, bool> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity, int Id)
        {
            var existEntity = await GetById(Id);
            _dbContext.Entry(existEntity).CurrentValues.SetValues(entity);
            _dbContext.Update(existEntity);
        }

        public DbSet<T> GetAll()
        {
            return _dbSet;
        }
    }
}
