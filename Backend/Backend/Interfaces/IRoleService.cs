using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<object> FilterRolesAsync(RoleFilterDto dto);
        Task<Result<string>> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<Role?> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
        Task<bool> DeactivateRoleAsync(int id);
        Task<bool> ActivateRoleAsync(int id);
    }
}
