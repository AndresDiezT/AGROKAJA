namespace RoleService.Api.Models
{
    public class Role
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool isActive { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
