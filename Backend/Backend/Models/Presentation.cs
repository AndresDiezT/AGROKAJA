using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Presentation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPresentation { get; set; }
        [Required(ErrorMessage = "El nombre de la Presentacion es obligatoria")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 30 caracteres")]
        public string NamePresentation { get; set; }
        [Required(ErrorMessage = "El nombre de la Abreviacion es obligatoria")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 10 caracteres")]
        public string AbbreviationPresentation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
