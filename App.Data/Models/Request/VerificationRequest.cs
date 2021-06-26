using System;

namespace App.Data.Models.Request
{
    public class VerificationRequest
    {
        public string VerificationCode { get; set; }
        public string Hash { get; set; }
        public Guid Uid { get; set; }
        public string Email { get; set; }
        public bool IsCreatePasswordVerification { get; set; }
    }
}
