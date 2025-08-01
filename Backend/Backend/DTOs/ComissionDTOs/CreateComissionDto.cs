using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.ComissionDTOs
{
    public class CreateComissionDto
    {
        [Required(ErrorMessage = "El nombre de la comision es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NameComission { get; set; }

        [Required(ErrorMessage = "El valor de la comision es obligatorio")]
        public decimal RateComission { get; set; }
    }
}
