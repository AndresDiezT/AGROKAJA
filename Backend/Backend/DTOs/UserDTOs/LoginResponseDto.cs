namespace Backend.DTOs.UserDTOs
{
    public class LoginResponseDto
    {
        public string? Email { get; set; }
        public int? Role { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
