using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using App.Data.Models;
using App.Data.Models.Results;

namespace App.Api.ActionFilters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validate model state
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    BadRequest(FetchModelError(context.ModelState)
                    ));
            }
        }

        private Result<ErrorLog> BadRequest(string message)
        {
            return new Result<ErrorLog>(false, System.Net.HttpStatusCode.BadRequest, message);
        }

        private string FetchModelError(ModelStateDictionary modelState)
        {
            return string.Join(Environment.NewLine, modelState.Values
                         .SelectMany(x => x.Errors)
                         .Select(x => x.ErrorMessage));
        }
    }
}
