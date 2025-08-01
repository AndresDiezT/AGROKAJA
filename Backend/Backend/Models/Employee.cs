using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmployee { get; set; }
        [Required(ErrorMessage = "El salario del empleado es obligatorio")]
        [Precision(18, 2)]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "La fecha de contratación es obligatoria")]
        public DateOnly HireDate { get; set; }
        
        //RELATIONS
        public string Document { get; set; }
        [ForeignKey("Document")]
        public User User { get; set; }
        [Required(ErrorMessage = "La ciudad del empleado es obligatoria")]
        public int IdCity { get; set; }
        [ForeignKey("IdCity")]
        public City City { get; set; }
    }
}
