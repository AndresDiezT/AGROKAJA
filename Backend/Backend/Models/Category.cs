using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
        public class Category
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdCategory { get; set; }
            [Required(ErrorMessage = "El nombre de la Categoría es obligatoria")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
            public string NameCategory { get; set; }
            public DateTime CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public bool IsActive { get; set; }

            public DateTime? DeactivatedAt { get; set; }
        }
    }
