using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.AddressDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class AddressService : IAddressService
    {
        private readonly BackendDbContext _context;

        public AddressService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Address>>> GetAllAddressesAsync()
        {
            var addresses = await _context.Addresses.ToListAsync();

            return Result<IEnumerable<Address>>.Ok(addresses);
        }

        public async Task<Result<IEnumerable<Address>>> GetAddressesByUserAsync(string document)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Document == document);

            if (user is null)
                return Result<IEnumerable<Address>>.Fail("Usuario no encontrado");

            var addresses = await _context.Addresses
                .Where(a => a.OwnerDocument == user.Document && a.IsActive)
                .Include(a => a.City)
                .ToListAsync();

            return Result<IEnumerable<Address>>.Ok(addresses);
        }

        public async Task<Result<Address>> GetAddressByIdAsync(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.City)
                .FirstOrDefaultAsync(a => a.IdCity == id);

            if (address is null)
                return Result<Address>.Fail("Dirección no encontrada");

            return Result<Address>.Ok(address);
        }

        public async Task<Result<Address>> UpdateAddressAsync(UpdateAddressDto updateAddressDto)
        {
            var existingAddress = await _context.Addresses
                .FirstOrDefaultAsync(a => a.IdAddress == updateAddressDto.IdAddress);

            if (existingAddress is null)
                return Result<Address>.Fail("Dirección no encontrada");

            // Codigo para verificar si se quiere establecer esta dirección como predeterminada y cambiarle el estado a la anterior predeterminada
            if (updateAddressDto.IsDefaultAddress)
            {
                var currentDefaultAddress = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.OwnerDocument == existingAddress.OwnerDocument
                    && a.IsDefaultAddress
                    && a.IdAddress != existingAddress.IdAddress);

                if (currentDefaultAddress is not null)
                {
                    currentDefaultAddress.IsDefaultAddress = false;
                }
            }

            existingAddress.IsDefaultAddress = updateAddressDto.IsDefaultAddress;
            existingAddress.StreetAddress = updateAddressDto.StreetAddress;
            existingAddress.PostalCodeAddress = updateAddressDto.PostalCodeAddress;
            existingAddress.IdCity = updateAddressDto.IdCity;
            existingAddress.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<Address>.Ok(existingAddress);
        }

        public async Task<Result<Address>> CreateAddressAsync(CreateAddressDto createAddressDto)
        {
            // Codigo para verificar si el usuario ya tiene direcciones registradas
            bool isFirstAddress = !await _context.Addresses
                .AnyAsync(a => a.OwnerDocument == createAddressDto.OwnerDocument);

            // Codigo para validar si ya existe una dirección idéntica para ese usuario
            bool duplicateExists = await _context.Addresses.AnyAsync(a =>
                a.StreetAddress == createAddressDto.StreetAddress &&
                a.PostalCodeAddress == createAddressDto.PostalCodeAddress &&
                a.OwnerDocument == createAddressDto.OwnerDocument);

            if (duplicateExists)
                return Result<Address>.Fail("La dirección ya está registrada para este usuario");

            var address = new Address
            {
                StreetAddress = createAddressDto.StreetAddress,
                PostalCodeAddress = createAddressDto.PostalCodeAddress,
                IsDefaultAddress = isFirstAddress,
                CreatedAt = DateTime.Now,
                IsActive = true,
                OwnerDocument = createAddressDto.OwnerDocument,
                IdCity = createAddressDto.IdCity
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Result<Address>.Ok(address);
        }


        public async Task<Result<bool>> DeactivateAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address is null)
                return Result<bool>.Fail("Dirección no encontrada");

            if (!address.IsActive)
                return Result<bool>.Fail("La dirección ya esta inactiva");

            address.IsActive = false;
            address.UpdatedAt = DateTime.Now;
            address.DeactivatedAt = DateTime.Now;

            _context.Entry(address).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address is null)
                return Result<bool>.Fail("Dirección no encontrada");

            if (address.IsActive)
                return Result<bool>.Fail("La dirección ya esta activa");

            address.IsActive = true;
            address.UpdatedAt = DateTime.Now;
            address.DeactivatedAt = null;

            _context.Entry(address).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
