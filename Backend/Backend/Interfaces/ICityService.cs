using Backend.DTOs;
using Backend.DTOs.CityDTOs;
using Backend.DTOs.CountryDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICityService
    {
        Task<Result<IEnumerable<City>>> GetAllCitiesAsync();
        Task<Result<City>> GetCityByIdAsync(int id);
        Task<Result<City>> CreateCityAsync(CreateCityDto createCityDto);
        Task<Result<City>> UpdateCityAsync(UpdateCityDto updateCityDto);
        Task<Result<bool>> DeactivateCityAsync(int id);
        Task<Result<bool>> ActivateCityAsync(int id);
    }
}
