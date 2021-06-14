using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Api.ActionFilters;
using App.Data.Model;
using App.Service;
using App.Data.Models.Results;

namespace App.Api.Controllers
{
    [Authorize]
    public class BaseVmController<T, Tvm, Tfilter, TResult> : BaseController<T> where T : class where Tvm : class where Tfilter : class where TResult : class
    {
        #region 'Initialization'
        protected readonly IVmAppService<T, Tvm, Tfilter, TResult> _vmService;
        #endregion

        #region 'Constructor'
        public BaseVmController(
            IVmAppService<T, Tvm, Tfilter, TResult> vmService, 
            ICallerService callerService) 
            : base(vmService, callerService)
        {
            _vmService = vmService;
        }
        #endregion

        #region 'CRUD Api'
        /// <summary>
        /// Get all class
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("get")]
        public async Task<Result<IEnumerable<TResult>>> GetAllAsync([FromBody] Tfilter model)
        {
            return Succeeded(await _vmService.GetAll(model));
        }

        /// <summary>
        /// Get record by Guid
        /// </summary>
        /// <returns></returns>
        [HttpGet("id/{uid}")]
        [ValidateModelState]
        public async Task<Result<TResult>> GetByUidAsync(string uid)
        {
            if (Guid.TryParse(uid, out Guid Uid))
            {
                return Succeeded(await _vmService.GetByUidAsync(Uid));
            }

            return null;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ValidateModelState]
        public async Task<Result<TResult>> CreateAsync([FromBody] Tvm model)
        {
            return Succeeded(await _vmService.AddAsync(model), Message.AddSuccess);
        }

        /// <summary>
        /// update record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}/update")]
        [ValidateModelState]
        public async Task<Result<TResult>> UpdateAsync([FromBody] Tvm model, long id)
        {
            return Succeeded(await _vmService.UpdateAsync(model, id), Message.UpdateSuccess);
        }
        #endregion
    }
}
