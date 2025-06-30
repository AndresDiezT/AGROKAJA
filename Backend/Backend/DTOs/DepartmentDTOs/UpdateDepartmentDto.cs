using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.DepartmentDTOs
{
    public class UpdateDepartmentDto
    {
        public int IdDepartment { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameDepartment { get; set; }

        // RELATIONS
        [Required(ErrorMessage = "El país es obligatorio")]
        public int IdCountry { get; set; }
    }
}
