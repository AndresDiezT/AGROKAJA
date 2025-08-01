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
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Este campo debe tener entre 2 y 5 caracteres")]
        public string CodeCountry { get; set; }
    }
}
