using Backend.DTOs.RoleDTOs;
using Backend.DTOs.TypeDocumentDto;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ITypeDocumentService
    {
        Task<IEnumerable<TypeDocument>> GetAllTypesDocumentAsync();
        Task<TypeDocument?> GetTypeDocumentByIdAsync(int id);
        Task<TypeDocument> CreateTypeDocumentAsync(CreateTypeDocumentDto createTypeDocumentDto);
        Task<TypeDocument?> UpdateTypeDocumentAsync(UpdateTypeDocumentDto updateTypeDocumentDto);
        Task<bool> DeactivateTypeDocumentAsync(int id);
        Task<bool> ActivateTypeDocumentAsync(int id);
    }
}
