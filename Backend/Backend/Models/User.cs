using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "El documento es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener un máximo 10 caracteres")]
        public string Document { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Este campo debe tener entre 10 y 255 caracteres")]
        [EmailAddress(ErrorMessage = "Este campo debe ser un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 y 100 caracteres")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El codigo del país es obligatorio")]
        [StringLength(5, ErrorMessage = "Este campo debe tener hasta 5 digitos numericos")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "El numero de celular es obligatorio")]
        [StringLength(10, ErrorMessage = "Este campo debe tener 10 digitos numericos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de celular debe tener 10 dígitos numéricos")]
        public string PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public bool EmailIsVerified { get; set; }
        public bool PhoneIsVerified { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? LastLoginIp { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateOnly BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // RELATIONS
        public int IdTypeDocument { get; set; }
        [ForeignKey("IdTypeDocument")]
        public TypeDocument TypeDocument { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
