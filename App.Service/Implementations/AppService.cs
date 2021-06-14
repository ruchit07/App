using App.Data.Models;
using App.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Service
{
    public class AppService<T> : BaseService, IAppService<T> where T : class
    {
        protected Data.Context.AppContext dbContext;
        private readonly IAppRepository<T> _repository;

        public AppService(
            ICallerService callerService,
            IAppRepository<T> repository
            ) : base(callerService)
        {
            _repository = repository;
            dbContext = _repository.dbContext;
        }

        #region 'Get'
        public virtual async Task<T> GetAsync(long id)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public virtual async Task<T> GetAsync(Guid uid)
        {
            return await _repository.GetAsync(uid);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(long? id = 0)
        {
            return await _repository.GetAllAsync(id);
        }

        public virtual async Task<IQueryable<T>> GetAllAsync(Filter filter)
        {
            return await _repository.GetAllAsync(filter);
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where)
        {
            return await _repository.Where(where);
        }

        public virtual async Task<IQueryable<T>> Where(Expression<Func<T, bool>> where, int skip, int take)
        {
            return await _repository.Where(where, skip, take);
        }
        #endregion

        #region 'Add'
        public virtual async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual async Task<T> AddWithoutSaveAsync(T entity)
        {
            return await _repository.AddWithoutSaveAsync(entity);
        }

        public virtual async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }
        #endregion

        #region 'Update'
        public virtual async Task<T> UpdateAsync(T entity, long id)
        {
            return await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<T> UpdateWithoutSaveAsync(T entity, long id)
        {
            return await _repository.UpdateWithoutSaveAsync(entity, id);
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            return await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }
        #endregion

        #region 'Delete'
        public virtual async Task<long> DeleteAsync(T entity, long id)
        {
            return await _repository.DeleteAsync(entity, id);
        }

        public virtual async Task<int> DeleteAsync(T entity, int id)
        {
            return await _repository.DeleteAsync(entity, id);
        }
        #endregion
    }
}
