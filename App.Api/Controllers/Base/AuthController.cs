using App.Data.Infrastructure;
using App.Data.Models.Request;
using App.Data.Models.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ZCoreEngine.Auth.Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        #region 'Service Initialization'
        private readonly IJwtAuthManager _jwtAuthManager;
        #endregion

        #region 'Constructor'
        public AuthController(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }
        #endregion

        #region 'Auth'
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<Result<LoginResult>> Authenticate([FromBody] LoginRequest loginRequest)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, ""),
                new Claim(JwtRegisteredClaimNames.Aud, ""),
            };

            var jwtResult = await _jwtAuthManager.GenerateTokens(new TokenRequest()
            {
                UserName = loginRequest.UserName,
                UserId = 0,
                Aud = "",
                Claims = claims
            });

            return new Result<LoginResult>(true, System.Net.HttpStatusCode.OK, new LoginResult()
            {
                AccessToken = jwtResult.AccessToken,
            });
        }
        #endregion
    }
}
