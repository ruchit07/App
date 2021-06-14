using App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Service
{
    public interface IAppService<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Guid uid);
        Task<IEnumerable<T>> GetAllAsync(long? id = 0);
        Task<IQueryable<T>> GetAllAsync(Filter filter);
        Task<T> AddAsync(T entity);
        Task<T> AddWithoutSaveAsync(T entity);
        Task<T> UpdateAsync(T entity, long id);
        Task<T> UpdateWithoutSaveAsync(T entity, long id);
        Task<T> UpdateAsync(T entity, int id);
        Task<T> UpdateAsync(T updated);
        Task SaveAsync();
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where, int skip, int take);
        Task<long> DeleteAsync(T entity, long id);
        Task<int> DeleteAsync(T entity, int id);
    }
}
