using Backend.DTOs.RoleDTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByDocumentAsync(string document);
        Task<User> CreateUserAsync(CreateUserDto createUserDto);
        Task<User?> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<bool> DeactivateUserAsync(string document);
        Task<bool> ActivateUserAsync(string document);
    }
}
