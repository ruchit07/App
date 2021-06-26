using System;

namespace App.Data.Models.Request
{
    public class ResetPasswordRequest
    {
        public Guid Uid { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ResetPasswordCode { get; set; }
    }
}
