using System.Text.Json.Serialization;

namespace App.Data.Models.Request
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
