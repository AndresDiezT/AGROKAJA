using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAddress { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Este campo debe tener entre 5 y 255 caracteres")]
        public string StreetAddress { get; set; }

        [StringLength(20, ErrorMessage = "Este campo debe tener hasta 20 caracteres")]
        public string PostalCodeAddress { get; set; }

        public bool IsDefaultAddress { get; set; }

        [Required(ErrorMessage = "El codigo del país es obligatorio")]
        [StringLength(5, ErrorMessage = "Este campo debe tener hasta 5 digitos")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "El numero de celular es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener 10 digitos numericos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de celular debe tener 10 dígitos numéricos")]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string AddressReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // RELATIONS
        public string UserDocument { get; set; }
        [ForeignKey("UserDocument")]
        public User User { get; set; }

        public int IdCity { get; set; }
        [ForeignKey("IdCity")]
        public City City { get; set; }
    }
}
