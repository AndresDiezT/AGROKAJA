using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.CountryDTOs
{
    public class CreateCountryDto
    {
        [Required(ErrorMessage = "El nombre del pais es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameCountry { get; set; }
        [Required(ErrorMessage = "El nombre del pais es obligatorio")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo debe tener entre 1 y 2 caracteres")]
        public string CodeCountry { get; set; }
    }
}
