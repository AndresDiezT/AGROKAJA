using Backend.DTOs.PresentationDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPresentationService
    {
        Task<IEnumerable<Presentation>> GetAllPresentationsAsync();
        Task<Presentation> GetPresentationByIdAsync(int idPresentation);
        Task<Presentation> CreatePresentationAsync(CreatePresentationDto createPresentationDto);
        Task<Presentation> UpdatePresentationAsync(int idPresentation, CreatePresentationDto updatePresentationDto);
        Task<bool> DeactivatePresentationAsync(int idPresentation);
        Task<bool> ActivatePresentationAsync(int idPresentation);
    }
}
