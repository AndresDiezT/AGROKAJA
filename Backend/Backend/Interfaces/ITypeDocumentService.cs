using Backend.DTOs;
using Backend.DTOs.RoleDTOs;
using Backend.DTOs.TypeDocumentDto;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ITypeDocumentService
    {
        Task<Result<IEnumerable<TypeDocument>>> GetAllTypesDocumentAsync();
        Task<Result<TypeDocument>> GetTypeDocumentByIdAsync(int id);
        Task<Result<TypeDocument>> CreateTypeDocumentAsync(CreateTypeDocumentDto createTypeDocumentDto);
        Task<Result<TypeDocument>> UpdateTypeDocumentAsync(UpdateTypeDocumentDto updateTypeDocumentDto);
        Task<Result<bool>> DeactivateTypeDocumentAsync(int id);
        Task<Result<bool>> ActivateTypeDocumentAsync(int id);
    }
}
