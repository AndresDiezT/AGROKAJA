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
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 255 caracteres")]
        public string StreetAddress { get; set; }

        [StringLength(20, ErrorMessage = "Este campo debe tener hasta 20 caracteres")]
        public string PostalCodeAddress { get; set; }

        public bool IsDefaultAddress { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // RELATIONS
        public string OwnerDocument { get; set; }
        [ForeignKey("OwnerDocument")]
        public User User { get; set; }

        public int IdCity { get; set; }
        [ForeignKey("IdCity")]
        public City City { get; set; }
    }
}
