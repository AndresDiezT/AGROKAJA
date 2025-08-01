using Backend.DTOs.AddressDTOs;
using Backend.DTOs;
using Backend.Models;
using Backend.DTOs.ComissionDTOs;

namespace Backend.Interfaces
{
    public interface IComissionService
    {
        Task<IEnumerable<Comission>> GetAllComissionsAsync();
        Task<Comission> GetComissionByIdAsync(int id);
        Task<Comission> CreateComissionAsync(CreateComissionDto dto);
        Task<Comission> UpdateComissionAsync(int id,CreateComissionDto dto);
        Task<bool> DeactivateComissionAsync(int id);
        Task<bool> ActivateComissionAsync(int id);
    }
}
