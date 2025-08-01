using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPermission { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string NamePermission { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string DescriptionPermission { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 50 caracteres")]
        public string ModulePermission { get; set; }
        public string GuardNamePermission { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // RELATIONS
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
