using Backend.DTOs;
using Backend.DTOs.UserDTOs;

namespace Backend.Interfaces
{
    public interface IAuthService
    {
        Task<Result<ReadUserDto>> RegisterAsync(RegisterDto registerDto);
        Task<Result<LoginResponseDto>> LoginAsync(LoginDto loginDto);
    }
}
