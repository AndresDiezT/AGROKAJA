using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class InvoiceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInvoiceItems { get; set; }
        public int QuantityInvoiceItem { get; set; }
        public decimal UnitPriceInvoiceItem { get; set; }
        public decimal TotalInvoiceItem { get; set; }
        //Relations
        public int IdInvoice { get; set; }
        [ForeignKey("IdInvoice")]
        public Invoice Invoice { get; set; }
        public int IdOrderDetail { get; set; }
        [ForeignKey("IdOrderDetail")]
        public OrderDetail OrderDetail { get; set; }
    }
}
