using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDepartment { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameDepartment { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // RELATIONS
        public int IdCountry { get; set; }
        [ForeignKey("IdCountry")]
        public Country Country { get; set; }
    }
}