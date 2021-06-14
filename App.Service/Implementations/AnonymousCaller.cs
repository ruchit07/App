using System;
using System.Security.Claims;

namespace App.Service
{
    public class AnonymousCaller : ICallerService
    {
        public Guid UserUid { get; }
        public Guid ProductUid { get; }
        public Guid CustomerUid { get; }
        public string Role { get; }

        public AnonymousCaller()
        {
            UserUid = Guid.Empty;
            Role = string.Empty;
            ProductUid = Guid.Empty;
            CustomerUid = Guid.Empty;
        }

        public ICallerService GetUserClaim()
        {
            var principal = ClaimsPrincipal.Current;
            return new AuthenticatedCaller(principal);
        }
    }
}
