using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.CategoryDTOs
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "El nombre de la Categoría es obligatoria")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NameCategory { get; set; }
    }
}
