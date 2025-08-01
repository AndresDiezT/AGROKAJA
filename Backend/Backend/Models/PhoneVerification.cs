using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class PhoneVerification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPhoneVerification { get; set; }
        public int Code { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }

        // RELATIONS
        public string UserDocument { get; set; }
        [ForeignKey("UserDocument")]
        public User User { get; set; }
    }
}
