using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class UserRole
    {
        public string UserDocument { get; set; }
        [ForeignKey("UserDocument")]
        public User User { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }
    }
}
