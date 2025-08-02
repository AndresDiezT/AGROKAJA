using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.RoleDTOs
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50, ErrorMessage = "Este campo debe tener un máximo 50 caracteres")]
        public string NameRole { get; set; }
    }
}
