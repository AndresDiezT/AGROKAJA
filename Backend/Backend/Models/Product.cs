using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Product
    {
        [Key]
        [Required(ErrorMessage = "El nombre de Codigo de Producto es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 20 caracteres")]
        public string CodeProduct { get; set; }
        [Required(ErrorMessage = "El nombre de Producto es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        [ForeignKey("SubCategory")]
        public int IdSubCategory { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}
