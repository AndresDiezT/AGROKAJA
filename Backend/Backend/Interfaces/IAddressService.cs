using Backend.DTOs;
using Backend.DTOs.AddressDTOs;
using Backend.DTOs.CityDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAddressService
    {
        Task<Result<IEnumerable<Address>>> GetAllAddressesAsync();
        Task<Result<IEnumerable<Address>>> GetAddressesByUserAsync(string document);
        Task<Result<Address>> GetAddressByIdAsync(int id);
        Task<Result<Address>> CreateAddressAsync(CreateAddressDto createAddressDto);
        Task<Result<Address>> UpdateAddressAsync(UpdateAddressDto updateAddressDto);
        Task<Result<bool>> DeactivateAddressAsync(int id);
        Task<Result<bool>> ActivateAddressAsync(int id);
    }
}
