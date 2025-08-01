using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.OrderStatusDTOs
{
    public class SaveOrderStatusDto
    {
        [Required(ErrorMessage = "El nombre de la orden es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 20 caracteres")]
        public string NameOrderStatus { get; set; }
    }
}
