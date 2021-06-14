using System.ComponentModel.DataAnnotations;
using App.Data.Model;
using App.Data.Models.Enums;

namespace App.Data.Models
{
    public class LeadAddress : ActiveCreateDeleteUpdate
    {
        public long LeadAddressId { get; set; }

        public AddressType AddressType { get; set; } = AddressType.Primary;

        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string Address1 { get; set; }

        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string Address2 { get; set; }

        [StringLength(200, ErrorMessage = Message.MaxLength200Error)]
        public string Address3 { get; set; }

        [StringLength(10, ErrorMessage = Message.MaxLength10Error)]
        public string Zip { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
    }
}
