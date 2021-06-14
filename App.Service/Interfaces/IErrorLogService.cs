using System;
using System.Threading.Tasks;

namespace App.Service
{
    public interface IErrorLogService
    {
        Task LogExceptionAsync(Exception ex);
        Task LogMessageAsync(string message);
    }
}
