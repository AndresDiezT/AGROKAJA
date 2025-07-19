using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.CityDTOs
{
    public class UpdateCityDto
    {
        public int IdCity { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es obligatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameCity { get; set; }

        // RELATIONS
        [Required(ErrorMessage = "El departamento es obligatorio")]
        public int IdDepartment { get; set; }
    }
}
