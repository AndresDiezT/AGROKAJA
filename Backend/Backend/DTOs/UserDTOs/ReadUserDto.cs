namespace Backend.DTOs.UserDTOs
{
    public class ReadUserDto
    {
        public string Document { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public int IdRole { get; set; }

        public string RoleName { get; set; }

        public int IdTypeDocument { get; set; }

        public string TypeDocumentName { get; set; }
    }
}
