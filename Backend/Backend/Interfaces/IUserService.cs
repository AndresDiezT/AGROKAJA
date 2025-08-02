using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models;
using System.Linq.Expressions;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<object> FilterUsersAsync(UserFilterDto dto, string userType);
        Task<User> GetUserByDocumentAsync(string document);
        Task<Result<string>> UpdateUserAsync(string document, UpdateUserDto updateUserDto);
        Task<Result<bool>> VerifyEmailCodeAsync(string documento, int code);
        Task<bool> DeactivateUserAsync(string document);
        Task<bool> ActivateUserAsync(string document);
    }
}
