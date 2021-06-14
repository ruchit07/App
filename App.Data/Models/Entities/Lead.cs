using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Data.Model;

namespace App.Data.Models
{
    public class Lead : UserActiveCreateDeleteUpdate
    {
        [Key, Required]
        public long LeadId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string FirstName { get; set; }

        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string MiddleName { get; set; }

        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(12, ErrorMessage = Message.PasswordValidationError)]
        public string Phone { get; set; }

        [StringLength(10, ErrorMessage = Message.MaxLength10Error)]
        public string CountryCode { get; set; }

        public long TypeId { get; set; }
        public long CategoryId { get; set; }
        public long StageId { get; set; }
        public float Rating { get; set; }

        [StringLength(20, ErrorMessage = Message.MaxLength20Error)]
        public string Stage { get; set; }

        [StringLength(20, ErrorMessage = Message.MaxLength20Error)]
        public string Source { get; set; }

        public string Tags { get; set; }
        public long AssignedTo { get; set; }
        public bool IsArchived { get; set; }
        public bool IsJunk { get; set; }

        #region 'References'
        public virtual ICollection<LeadAddress> Addresses { get; set; }
        public virtual ICollection<LeadNote> Notes { get; set; }
        public virtual LeadSource LeadSource { get; set; }
        #endregion
    }
}
