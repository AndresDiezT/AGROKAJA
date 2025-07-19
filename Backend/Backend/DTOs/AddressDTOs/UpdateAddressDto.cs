using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.AddressDTOs
{
    public class UpdateAddressDto
    {
        public int IdAddress { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 255 caracteres")]
        public string StreetAddress { get; set; }

        [StringLength(100, ErrorMessage = "Este campo debe tener hasta 20 caracteres")]
        public string PostalCodeAddress { get; set; }
        public bool IsDefaultAddress { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatoría es obligatorio")]
        public int IdCity { get; set; }
    }
}
