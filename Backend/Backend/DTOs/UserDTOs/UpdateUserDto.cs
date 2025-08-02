using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El numero de celular es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener 10 digitos numericos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de celular debe tener 10 dígitos numéricos")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime BirthDate { get; set; }
    }
}
