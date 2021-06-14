using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using App.Data.Model;
using App.Data.Models.Results;

namespace App.Api.Controllers
{
    public class BaseActionController : Controller
    {
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
