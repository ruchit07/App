using App.Data.Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Data.Infrastructure
{
    public interface IJwtAuthManager
    {
        Task<JwtAuthResult> GenerateTokens(TokenRequest tokenRequest);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);

    }
}
