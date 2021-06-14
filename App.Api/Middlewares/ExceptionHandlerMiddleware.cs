using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using App.Data.Context;
using App.Data.Models;
using App.Service;
using App.Data.Infrastructure;
using App.Data.Models.Results;

namespace App.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _requestDelegate;
        private readonly IErrorLogService _errorLogService;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IUnitOfWork<Data.Context.AppContext> _unitOfWork;

        /// <summary>
        /// Initialize a new instance
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(
            RequestDelegate requestDelegate, 
            IErrorLogService errorLogService,
            IUnitOfWork<Data.Context.AppContext> unitOfWork)
        {
            _requestDelegate = requestDelegate;
            _errorLogService = errorLogService;
            _unitOfWork = unitOfWork;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        /// <summary>
        /// Invoke context
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                var httpStatusCode = await ConfigurateExceptionTypes(exception);

                // set http status code and content type
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = JsonContentType;

                // writes / returns error model to the response
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new Result<ErrorLog>
                    {
                        IsSuccess = false,
                        Message = exception.Message,
                        StatusCode = httpStatusCode,
                        StatusMessage = httpStatusCode.ToString(),
                        Model = null
                    }, _serializerSettings));

                //context.Response.Headers.Clear();
            }
        }

        private async Task<HttpStatusCode> ConfigurateExceptionTypes(Exception exception)
        {
            HttpStatusCode httpStatusCode;

            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case var _ when exception is InvalidOperationException:
                    httpStatusCode = HttpStatusCode.OK;
                    break;
                case var _ when exception is KeyNotFoundException:
                    httpStatusCode = HttpStatusCode.OK;
                    break;
                case var _ when exception is UnauthorizedAccessException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    await _errorLogService.LogExceptionAsync(exception);
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            return httpStatusCode;
        }
    }
}
