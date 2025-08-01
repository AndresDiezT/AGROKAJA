using Backend.DTOs;
using Backend.DTOs.UserDTOs;

namespace Backend.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(RegisterDto registerDto);
        Task<Result<string>> AddEmployeeAsync(AddEmployeeDto addEmployeeDto);
        Task<Result<string>> ResendCredentialsAsync(string document);
        Task<Result<string>> VerifyEmailAsync(string document, int code);
        Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
        Task<Result<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<Result<LoginResponseDto>> RefreshTokenAsync();
        Task<Result<string>> LogoutAsync();
        Task<Result<ProfileDto>> GetProfileAsync();
    }
}
