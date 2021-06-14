using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Service
{
    public interface IVmAppService<T, Tvm, Tfilter, TResult> : IAppService<T> where T : class where Tvm : class where Tfilter : class where TResult : class
    {
        Task<IEnumerable<TResult>> GetAll(Tfilter filter);
        Task<TResult> GetByUidAsync(Guid uid);
        Task<TResult> AddAsync(Tvm entity);
        Task<TResult> UpdateAsync(Tvm entity, long id);
    }
}
