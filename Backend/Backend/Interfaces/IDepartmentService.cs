using Backend.DTOs;
using Backend.DTOs.DepartmentDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IDepartmentService
    {
        Task<Result<IEnumerable<Department>>> GetAllDepartmentsAsync();
        Task<Result<Department>> GetDepartmentByIdAsync(int id);
        Task<Result<Department>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task<Result<Department>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
        Task<Result<bool>> DeactivateDepartmentAsync(int id);
        Task<Result<bool>> ActivateDepartmentAsync(int id);
    }
}
