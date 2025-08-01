using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "La cantidad total del pedido es obligatoria")]

        public decimal TotalAmountOrder { get; set; }
        public string UserDocument { get; set; }
        public int IdAddress { get; set; }
        public int IdOrderStatus { get; set; }
        public int IdComission { get; set; }
        public int IdPaymentMethod { get; set; }
        public int IdPaymentStatus { get; set; }
    }
}
