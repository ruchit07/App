using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using App.Service;
using App.Data.Model;

namespace App.Api.Attributes
{
    public class AdminAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public AdminAttribute()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException(Message.Unauthorized);

            var _callerService = context.HttpContext.RequestServices.GetService(typeof(ICallerService)) as ICallerService;
        }
    }
}
