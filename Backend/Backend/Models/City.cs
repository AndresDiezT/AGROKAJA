using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCity { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es obligatoria")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Este campo debe tener entre 3 y 100 caracteres")]
        public string NameCity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        // RELATIONS
        public int IdDepartment { get; set; }
        [ForeignKey("IdDepartment")]
        public Department Department { get; set; }
    }
}