using App.Data.Model;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Security.Claims;

namespace App.Service
{
    public class AuthenticatedCaller : ICallerService
    {
        public Guid UserUid { get; }
        public Guid ProductUid { get; }
        public Guid CustomerUid { get; }
        public string Role { get; }

        public AuthenticatedCaller(ClaimsPrincipal principal)
        {
            UserUid = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Sid).Value);
            Role = Convert.ToString(principal.FindFirst(ClaimTypes.Role).Value);
            ProductUid = Guid.Parse(principal.FindFirst(Constant.Claim.Product).Value);
            CustomerUid = Guid.Parse(principal.FindFirst(Constant.Claim.Customer).Value);
        }

        public ICallerService GetUserClaim()
        {
            var principal = ClaimsPrincipal.Current;
            return new AuthenticatedCaller(principal);
        }
    }
}
