using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.Data.Models.Request
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Key { get; set; }

        public Guid ProductUid { get; set; }

        public Guid CustomerUid { get; set; }
    }
}
