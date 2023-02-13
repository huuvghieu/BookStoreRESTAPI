using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetWhere(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null);
        DbSet<T> GetAll();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity,int Id);
        Task<T> GetById(int id);
        Task RemoveAsync(T entity);
    }
}
