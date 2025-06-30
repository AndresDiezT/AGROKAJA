using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.TypeDocumentDto
{
    public class UpdateTypeDocumentDto
    {
        public int IdTypeDocument { get; set; }
        [Required(ErrorMessage = "El nombre del tipo de documento es obligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 2 y 5 caracteres")]
        public string NameTypeDocument { get; set; }
    }
}
