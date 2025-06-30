using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCountry { get; set; }

        [Required(ErrorMessage = "El nombre del pais es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameCountry { get; set; }

        [Required(ErrorMessage = "El nombre del pais es obligatorio")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo debe tener entre 1 y 2 caracteres")]
        public string CodeCountry { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }
    }
}