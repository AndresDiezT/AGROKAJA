using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.OrderDTOs
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "La cantidad total del pedido es obligatoria")]

        public decimal TotalAmountOrder { get; set; }
    }
}
