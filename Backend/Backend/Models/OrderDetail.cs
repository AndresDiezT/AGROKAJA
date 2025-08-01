using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrderDetail { get; set; }
        [Required(ErrorMessage = "La cantidad total del pedido es obligatoria")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "La cantidad total del pedido es obligatoria")]
        public decimal UnitPrice { get; set; }
        //Relations
        public int IdOrder { get; set; }
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }
        public int IdPost { get; set; }
        //[ForeignKey("IdPost")]
        //public Post Post { get; set; }
    }
}
