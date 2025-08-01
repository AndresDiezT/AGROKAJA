using Backend.DTOs;
using Backend.DTOs.AddressDTOs;
using Backend.DTOs.CityDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAddressService
    {
        Task<Result<IEnumerable<Address>>> GetAllAddressesAsync();
        Task<Result<IEnumerable<ReadAddressDto>>> GetAddressesByUserAsync(string document);
        Task<Result<Address>> GetAddressByIdAsync(int id, string userDocument);
        Task<Result<Address>> CreateAddressAsync(CreateAddressDto createAddressDto);
        Task<Result<Address>> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto);
        Task<Result<bool>> DeactivateAddressAsync(int id);
        Task<Result<bool>> ActivateAddressAsync(int id);
    }
}
