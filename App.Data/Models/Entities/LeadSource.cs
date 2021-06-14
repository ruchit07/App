using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Model;

namespace App.Data.Models
{
    public class LeadSource : Delete
    {
        [Key, Required]
        public long LeadSourceId { get; set; }

        [StringLength(5, ErrorMessage = Message.CodeLength5Error)]
        public string SourceInfoCode { get; set; }

        [StringLength(10, ErrorMessage = Message.MaxLength10Error)]
        public string SourceInfoId { get; set; }

        public string SourceData { get; set; }
    }
}
