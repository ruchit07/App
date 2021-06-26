using System.ComponentModel.DataAnnotations;

namespace App.Data.Models.Request
{
    public class AccountCheckRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
