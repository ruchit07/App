using System.ComponentModel.DataAnnotations;
using App.Data.Model;

namespace App.Data.Models
{
    public class LeadNote : ActiveCreateDeleteUpdate
    {
        [Key, Required]
        public long LeadNoteId { get; set; }

        public string Note { get; set; }
    }
}
