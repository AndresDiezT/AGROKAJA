using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Comission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComission { get; set; }

        [Required(ErrorMessage = "El nombre de la comision es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NameComission { get; set; }

        [Required(ErrorMessage = "El valor de la comision es obligatorio")]
        public decimal RateComission { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }
    }
}
