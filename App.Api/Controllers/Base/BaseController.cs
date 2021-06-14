using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Api.ActionFilters;
using App.Service;
using System.Reflection;
using App.Data.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using App.Data.Models.Results;
using App.Data.Models;

namespace App.Api.Controllers
{
    public class BaseController<T> : BaseActionController where T : class
    {
        #region 'Initialization'
        protected readonly IAppService<T> _service;
        protected readonly ICallerService _callerService;
        #endregion

        #region 'Constructor'
        public BaseController(IAppService<T> service, ICallerService callerService)
        {
            _service = service;
            _callerService = callerService;
        }
        #endregion

        #region 'CRUD Api'
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateModelState]
        public virtual async Task<Result<IEnumerable<T>>> GetAll()
        {
            return Succeeded(await _service.GetAllAsync());
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        [HttpGet("filtered")]
        [ValidateModelState]
        public virtual async Task<Result<List<T>>> GetAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string query)
        {
            var filter = new Filter()
            {
                Page = page,
                PageSize = pageSize,
                Query = query == "undefined" ? string.Empty : query,
            };
            return Succeeded((await _service.GetAllAsync(filter)).ToList());
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateModelState]
        public virtual async Task<Result<T>> Get(long id)
        {
            return Succeeded(await _service.GetAsync(id));
        }

        /// <summary>
        /// Get record by Guid
        /// </summary>
        /// <returns></returns>
        [HttpGet("uid/{uid}")]
        [ValidateModelState]
        public virtual async Task<Result<T>> GetByUid(string uid)
        {
            if (Guid.TryParse(uid, out Guid Uid))
            {
                return Succeeded(await _service.GetAsync(Uid));
            }

            return null;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelState]
        public virtual async Task<Result<T>> Create([FromBody] T model)
        {
            return Succeeded(await _service.AddAsync(model), Message.AddSuccess);
        }

        /// <summary>
        /// Update item by id
        /// </summary>
        /// <param name="model">Record to update</param>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModelState]
        public virtual async Task<Result<T>> Update([FromBody] T model, long id)
        {
            var entity = await _service.GetAsync(id);
            if (entity == null)
                return NotFound<T>(Message.UpdateError);

            return Succeeded(await _service.UpdateAsync(model, id), Message.UpdateSuccess);
        }

        /// <summary>
        /// Delete record by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateModelState]
        public virtual async Task<Result<T>> Delete(long id)
        {
            if (id <= 0)
                throw new ArgumentNullException(nameof(id));

            var entity = await _service.GetAsync(id);
            if (entity == null)
                return NotFound<T>(Message.DeleteError);

            await _service.DeleteAsync(entity, id);

            return Succeeded<T>(Message.DeleteSuccess);
        }
        #endregion

        #region 'No Action'
        [NonAction]
        protected Result<TEntity> Succeeded<TEntity>(TEntity result) where TEntity : class
        {
            return new Result<TEntity>(true, System.Net.HttpStatusCode.OK, result);
        }

        [NonAction]
        protected Result<TEntity> Succeeded<TEntity>(string message) where TEntity : class
        {
            return new Result<TEntity>(true, System.Net.HttpStatusCode.OK, message);
        }

        [NonAction]
        protected Result<TEntity> Succeeded<TEntity>(TEntity result, string message) where TEntity : class
        {
            return new Result<TEntity>(true, System.Net.HttpStatusCode.OK, message, result);
        }

        [NonAction]
        protected Result<TEntity> Failed<TEntity>() where TEntity : class
        {
            return new Result<TEntity>(false, System.Net.HttpStatusCode.InternalServerError, Message.SomethingWentWrong);
        }

        [NonAction]
        protected Result<TEntity> Failed<TEntity>(string message) where TEntity : class
        {
            if (string.IsNullOrEmpty(message))
                message = Message.SomethingWentWrong;

            return new Result<TEntity>(false, System.Net.HttpStatusCode.InternalServerError, message);
        }

        [NonAction]
        protected Result<TEntity> BadRequest<TEntity>(string message) where TEntity : class
        {
            return new Result<TEntity>(false, System.Net.HttpStatusCode.BadRequest, message);
        }

        [NonAction]
        protected Result<TEntity> Unauthorized<TEntity>() where TEntity : class
        {
            return new Result<TEntity>(false, System.Net.HttpStatusCode.Unauthorized, Message.Unauthorized);
        }

        [NonAction]
        protected Result<TEntity> NotFound<TEntity>() where TEntity : class
        {
            return new Result<TEntity>(false, System.Net.HttpStatusCode.NotFound, Message.NotFound);
        }

        [NonAction]
        protected Result<TEntity> NotFound<TEntity>(string message) where TEntity : class
        {
            return new Result<TEntity>(false, System.Net.HttpStatusCode.NotFound, message);
        }

        [NonAction]
        protected string FetchModelError(ModelStateDictionary modelState)
        {
            return string.Join(Environment.NewLine, modelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
        }
        #endregion
    }
}
