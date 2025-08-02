using Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTOs.AddressDTOs
{
    public class ReadAddressDto
    {
        public int IdAddress { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCodeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressReference { get; set; }
        public bool IsDefaultAddress { get; set; }
        public bool IsActive { get; set; }

        // RELATIONS
        public int IdCity { get; set; }
        public string NameCity { get; set; }
        public int IdDepartment { get; set; }
        public string NameDepartment { get; set; }
    }
}
