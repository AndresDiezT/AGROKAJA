using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class RolePermission
    {
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }

        public int IdPermission { get; set; }
        [ForeignKey("IdPermission")]
        public Permission Permission { get; set; }
    }
}
