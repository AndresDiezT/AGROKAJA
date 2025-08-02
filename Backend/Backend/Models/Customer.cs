using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCustomer { get; set; }
        public int TotalSales { get; set; }
        public int TotalPurchases { get; set; }
        public int ReputationScore { get; set; }
        //RELATIONS
        [Required]
        public string Document { get; set; }
        [ForeignKey("Document")]
        public User User { get; set; }

    }
}