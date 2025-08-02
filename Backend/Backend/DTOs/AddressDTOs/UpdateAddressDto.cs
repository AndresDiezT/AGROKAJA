using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.AddressDTOs
{
    public class UpdateAddressDto
    {
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Este campo debe tener entre 5 y 255 caracteres")]
        public string StreetAddress { get; set; }

        [StringLength(20, ErrorMessage = "Este campo debe tener hasta 20 caracteres")]
        public string PostalCodeAddress { get; set; }

        [Required]
        public bool IsDefaultAddress { get; set; }

        [Required(ErrorMessage = "El numero de celular es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener 10 digitos numericos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de celular debe tener 10 dígitos numéricos")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Añade una referencía")]
        [StringLength(255)]
        public string AddressReference { get; set; }

        // RELATIONS

        [Required(ErrorMessage = "La ciudad es obligatoría")]
        public int IdCity { get; set; }
        [Required]
        public string UserDocument { get; set; }
    }
}
