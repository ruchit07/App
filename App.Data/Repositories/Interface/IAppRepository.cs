using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Data.Models;

namespace App.Data.Repositories
{
    public interface IAppRepository<T> where T : class
    {
        Data.Context.AppContext dbContext { get; }
        Task<T> GetAsync(long id);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Guid uid);
        Task<IEnumerable<T>> GetAllAsync(long? schoolId = 0);
        Task<IQueryable<T>> GetAllAsync(Filter filter);
        Task<T> AddAsync(T entity);
        Task<T> AddWithoutSaveAsync(T entity);
        Task<T> UpdateAsync(T entity, long key);
        Task<T> UpdateWithoutSaveAsync(T updated, long key);
        Task<T> UpdateAsync(T entity, int key);
        Task<T> UpdateAsync(T updated);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where);
        Task<IQueryable<T>> Where(Expression<Func<T, bool>> where, int skip, int take);
        Task<long> DeleteAsync(T entity);
        Task<long> DeleteAsync(T entity, long id);
        Task<int> DeleteAsync(T entity, int id);
        Task<long> DeleteAsync(Expression<Func<T, bool>> where);
        IEnumerable<TResult> ExecuteSP<TResult>(string query, params SqlParameter[] SqlPrms) where TResult : new();
        Task TruncateAsync(String TableName);
        Task SaveAsync();
        Task BeginTransaction();
        Task Rollback();
        Task CommitAsync();
    }
}
