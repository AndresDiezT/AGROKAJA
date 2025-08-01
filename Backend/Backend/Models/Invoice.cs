using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInvoice { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        //Relations
        public int IdOrder{ get; set; }
        [ForeignKey("IdOrder")]
        public Order Order { get; set; }
    }
}
