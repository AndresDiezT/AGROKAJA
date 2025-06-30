using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Este campo debe tener entre 10 y 255 caracteres")]
        [EmailAddress(ErrorMessage = "Este campo debe ser un correo válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 y 50 caracteres")]
        public string Password { get; set; }
    }
}