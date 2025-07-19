using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class PostMedia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPostMedia { get; set; }
        
        [Required(ErrorMessage = "El nombre del Tipo de Archivo es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 20 caracteres")]
        public string MediaType { get; set; }

        [Required(ErrorMessage = "La Url es obligatoria")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 500 caracteres")]
        public string FileUrl { get; set; }

        [Required(ErrorMessage = "El nombre del Archivo es obligatorio")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 255 caracteres")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "La Posicion es obligatoria")]
        public int Position { get; set; }

        public Boolean IsMain { get; set; }

        public DateTime UploadedAt { get; set; }

        public int IdPost { get; set; }
        [ForeignKey("IdPost")]
        public Post Post { get; set; }
    }
}
