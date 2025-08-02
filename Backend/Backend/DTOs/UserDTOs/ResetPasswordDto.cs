using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserDTOs
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 y 50 caracteres")]
        public string NewPassword { get; set; }
    }
}
