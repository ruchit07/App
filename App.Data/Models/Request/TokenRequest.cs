using System.Security.Claims;

namespace App.Data.Models.Request
{
    public class TokenRequest
    {
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string Aud { get; set; }
        public Claim[] Claims { get; set; }
    }
}
