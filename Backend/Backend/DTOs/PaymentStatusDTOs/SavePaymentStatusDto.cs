using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.PaymentStatusDTOs
{
    public class SavePaymentStatusDto
    {
        [Required(ErrorMessage = "El nombre del método de pago es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NamePaymentStatus { get; set; }
    }
}
