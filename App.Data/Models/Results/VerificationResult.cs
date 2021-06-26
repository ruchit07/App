using System.Text.Json.Serialization;

namespace App.Data.Models.Results
{
    public class VerificationResult
    {
        public bool IsVerified { get; set; }
        public bool IsForcePasswordReset { get; set; }
        public string ResetPasswordCode { get; set; }
        public string RedirectUri { get; set; }
    }
}
