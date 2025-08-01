using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserDTOs
{
    public class RegisterDto
    {
        [Key]
        [Required(ErrorMessage = "El documento es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener un máximo 10 caracteres")]
        public string Document { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Este campo debe tener entre 10 y 255 caracteres")]
        [EmailAddress(ErrorMessage = "Este campo debe ser un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 y 100 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El codigo del país es obligatorio")]
        [StringLength(5, ErrorMessage = "Este campo debe tener hasta 5 digitos numericos")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "El numero de celular es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener 10 digitos numericos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de celular debe tener 10 dígitos numéricos")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateOnly BirthDate { get; set; }

        // RELATIONS
        [Required(ErrorMessage = "El rol es obligatorio")]
        public int IdRole { get; set; }
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int IdTypeDocument { get; set; }
    }
}
