using System.Text.Json.Serialization;

namespace App.Data.Models.Request
{
    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
