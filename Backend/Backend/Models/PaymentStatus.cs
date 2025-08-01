using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PaymentStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPaymentStatus { get; set; }
        [Required(ErrorMessage = "El nombre de metodo de pago es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NamePaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }
    }
}
