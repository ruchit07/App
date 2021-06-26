using System.Text.Json.Serialization;

namespace App.Data.Infrastructure
{
    public class JwtAuthResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public JwtRefreshToken RefreshToken { get; set; }
    }
}
