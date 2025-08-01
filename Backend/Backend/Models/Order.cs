using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = "La cantidad total del pedido es obligatoria")]

        public decimal TotalAmountOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }
        //Relations
        public string UserDocument { get; set; }
        [ForeignKey("UserDocument")]
        public User User { get; set; }
        public int IdAddress { get; set; }
        [ForeignKey("IdAddress")]
        public Address Address { get; set; }
        public int IdOrderStatus { get; set; }
        [ForeignKey("IdOrderStatus")]
        public OrderStatus OrderStatus { get; set; }
        public int IdComission { get; set; }
        [ForeignKey("IdComission")]
        public Comission Comission { get; set; }
        public int IdPaymentMethod { get; set; }
        [ForeignKey("IdPaymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }
        public int IdPaymentStatus { get; set; }
        [ForeignKey("IdPaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
