using System;

namespace App.Data.Models.Results
{
    public class UserResult
    {
        public long UserId { get; set; }
        public Guid Uid { get; set; }
        public Guid ProductId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? EmailVerifiedOn { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? PhoneVerifiedOn { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? BlockedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
