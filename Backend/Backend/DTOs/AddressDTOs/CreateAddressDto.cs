using Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTOs.AddressDTOs
{
    public class CreateAddressDto
    {

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 255 caracteres")]
        public string StreetAddress { get; set; }

        [StringLength(100, ErrorMessage = "Este campo debe tener hasta 20 caracteres")]
        public string PostalCodeAddress { get; set; }

        // RELATIONS
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string OwnerDocument { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoría es obligatorio")]
        public int IdCity { get; set; }
    }
}
