using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICountryService
    {
        Task<Result<IEnumerable<ReadCountryDto>>> GetAllCountriesAsync();
        Task<object> FilterCountriesAsync(CountryFilterDto dto);
        Task<Result<Country>> GetCountryByIdAsync(int id);
        Task<Result<Country>> CreateCountryAsync(CreateCountryDto createCountryDto);
        Task<Result<Country>> UpdateCountryAsync(UpdateCountryDto updateCountryDTO);
        Task<Result<bool>> DeactivateCountryAsync(int id);
        Task<Result<bool>> ActivateCountryAsync(int id);
    }
}