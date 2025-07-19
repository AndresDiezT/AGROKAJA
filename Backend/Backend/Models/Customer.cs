using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "El documento es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener un máximo 10 caracteres")]
        public string Document { get; set; }
        
    }
}