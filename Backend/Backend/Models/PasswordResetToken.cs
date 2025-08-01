using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class PasswordResetToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }

        // RELATIONS
        public string UserDocument { get; set; }
        [ForeignKey("UserDocument")]
        public User User { get; set; }
    }
}
