using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Post
    {
        public int IdPost { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required(ErrorMessage = "El título de la publicación es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string TitlePost { get; set; }

        [Required(ErrorMessage = "La descripción de la publicación es obligatoria")]
        public string DescriptionPost { get; set; }

        [Required(ErrorMessage = "La cantidad de la publicación es obligatoria")]
        public int QuantityPost { get; set; }

        [Required(ErrorMessage = "La cantidad por unidad es obligatoria")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal QuantityPerUnitPost { get; set; }

        [Required(ErrorMessage = "El precio por unidad es obligatorio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerUnitPost { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        public string CodeProduct { get; set; }

        public string SellerDocument { get; set; }

        public int IdPresentation { get; set; }

        public Product Product { get; set; }

        public User Seller { get; set; }

        public Presentation Presentation { get; set; }
    }
}
