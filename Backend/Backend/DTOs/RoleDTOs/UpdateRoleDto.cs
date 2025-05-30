using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.RoleDTOs
{
    public class UpdateRoleDto
    {
        [Required(ErrorMessage = "El identificador del rol es obligatorio")]
        public int IdRole { get; set; }
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(20, ErrorMessage = "Este campo debe tener un máximo de 20 caracteres")]
        public string NameRole { get; set; }
    }
}
