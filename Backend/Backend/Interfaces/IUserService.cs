using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<Result<ReadUserDto>> GetProfileAsync();
        Task<User> GetUserByDocumentAsync(string document);
        Task<User> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<bool> DeactivateUserAsync(string document);
        Task<bool> ActivateUserAsync(string document);

        Task<object> FilterUsersAsync(UserFilterDto filterUserDto);
    }
}
