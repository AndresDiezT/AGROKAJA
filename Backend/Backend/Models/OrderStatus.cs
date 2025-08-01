using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrderStatus { get; set; }

        [Required(ErrorMessage = "El nombre de la orden es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 20 caracteres")]
        public string NameOrderStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }
    }
}
