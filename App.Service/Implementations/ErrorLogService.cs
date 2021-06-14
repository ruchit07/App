using App.Data.Helpers;
using App.Data.Models;
using App.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace App.Service
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IAppRepository<ErrorLog> _erorrLogRepository;

        public ErrorLogService(IAppRepository<ErrorLog> errorLogRepository)
        {
            _erorrLogRepository = errorLogRepository;
        }

        public async Task LogExceptionAsync(Exception ex)
        {
            await _erorrLogRepository.AddAsync(new ErrorLog()
            {
                CreatedTime = Helper.GetCurrentDateTime(),
                InnerException = ex.InnerException?.Message ?? null,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
            });
        }

        public async Task LogMessageAsync(string message)
        {
            await _erorrLogRepository.AddAsync(new ErrorLog()
            {
                CreatedTime = Helper.GetCurrentDateTime(),
                Message = message
            });
        }
    }
}
