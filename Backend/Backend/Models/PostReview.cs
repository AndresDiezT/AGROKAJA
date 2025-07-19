using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class PostReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReview { get; set; }
        public string MessageReview { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        [Required(ErrorMessage = "El nombre del Cliente es obligatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 10 caracteres")]
        public string CustomerDocument { get; set; }
        [Required(ErrorMessage = "El nombre del Codigo de Producto es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 20 caracteres")]
        public string CodeProduct { get; set; }
    }
}
