using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Models;
using App.Data.Repositories;
using App.Service.Extention;

namespace App.Service
{
    public class VmAppService<T, Tvm, Tfilter, TResult> : AppService<T>, IVmAppService<T, Tvm, Tfilter, TResult> where T : class where Tvm : class where Tfilter : class where TResult : class
    {
        #region 'Service'
        private readonly IAppRepository<T> _repository;
        #endregion

        #region 'Constructor'
        public VmAppService(
            ICallerService callerService,
            IAppRepository<T> repository) 
            : base(callerService, repository)
        {
            _repository = repository;
        }
        #endregion

        public virtual async Task<IEnumerable<TResult>> GetAll(Tfilter filter)
        {
            return (await base.GetAllAsync(filter as Filter)).ToList().ToResult<T, TResult>();
        }

        public virtual async Task<TResult> GetByUidAsync(Guid uid)
        {
            return (await base.GetAsync(uid)).ToResult<T, TResult>();
        }

        public virtual async Task<TResult> AddAsync(Tvm model)
        {
            model.ToValidate<Tvm>();
            return (await base.AddAsync(model.ToEntity<T, Tvm>())).ToResult<T, TResult>();
        }

        public virtual async Task<TResult> UpdateAsync(Tvm model, long id)
        {
            model.ToValidate<Tvm>();
            return (await base.UpdateAsync(model.ToEntity<T, Tvm>())).ToResult<T, TResult>();
        }
    }
}
