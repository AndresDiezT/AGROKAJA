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

        public async Task<Result<IEnumerable<ReadAddressDto>>> GetAddressesByUserAsync(string document)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Document == document);

            if (user is null)
                return Result<IEnumerable<ReadAddressDto>>.Fail("Usuario no encontrado");

            var addresses = await _context.Addresses
                .Where(a => a.UserDocument == user.Document && a.IsActive)
                .Include(a => a.City)
                    .ThenInclude(c => c.Department)
                .ToListAsync();

            var dto = addresses.Select(a => new ReadAddressDto
            {
                IdAddress = a.IdAddress,
                StreetAddress = a.StreetAddress,
                PostalCodeAddress = a.PostalCodeAddress,
                IsDefaultAddress = a.IsDefaultAddress,
                IsActive = a.IsActive,
                IdCity = a.City?.IdCity ?? 0,
                NameCity = a.City?.NameCity ?? string.Empty,
                IdDepartment = a.City?.Department?.IdDepartment ?? 0,
                NameDepartment = a.City?.Department?.NameDepartment ?? string.Empty
            }).ToList();

            return Result<IEnumerable<ReadAddressDto>>.Ok(dto);
        }

        public async Task<Result<Address>> GetAddressByIdAsync(int id, string userDocument)
        {
            var address = await _context.Addresses
                .Include(a => a.City)
                .FirstOrDefaultAsync(a => a.IdAddress == id && a.UserDocument == userDocument);

            if (address is null)
                return Result<Address>.Fail("Dirección no encontrada");

            return Result<Address>.Ok(address);
        }

        public async Task<Result<Address>> UpdateAddressAsync(int id, UpdateAddressDto updateAddressDto)
        {
            var existingAddress = await _context.Addresses
                .FirstOrDefaultAsync(a => a.IdAddress == id);

            if (existingAddress is null)
                return Result<Address>.Fail("Dirección no encontrada");

            // Codigo para validar si ya existe una dirección idéntica para ese usuario
            bool duplicateExists = await _context.Addresses.AnyAsync(a =>
                a.IsActive &&
                a.IdAddress != id &&
                a.StreetAddress == updateAddressDto.StreetAddress &&
                a.PostalCodeAddress == updateAddressDto.PostalCodeAddress &&
                a.UserDocument == updateAddressDto.UserDocument &&
                a.PhoneNumber == updateAddressDto.PhoneNumber);

            if (duplicateExists)
                return Result<Address>.Fail("Esta dirección ya esta registrada");

            // Codigo para verificar si se quiere establecer esta dirección como predeterminada y cambiarle el estado a la anterior predeterminada
            if (updateAddressDto.IsDefaultAddress)
            {
                var currentDefaultAddress = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserDocument == existingAddress.UserDocument
                    && a.IsDefaultAddress
                    && id != existingAddress.IdAddress);

                if (currentDefaultAddress is not null)
                {
                    currentDefaultAddress.IsDefaultAddress = false;
                }
            }

            existingAddress.IsDefaultAddress = updateAddressDto.IsDefaultAddress;
            existingAddress.StreetAddress = updateAddressDto.StreetAddress;
            existingAddress.AddressReference = updateAddressDto.AddressReference;
            existingAddress.PostalCodeAddress = updateAddressDto.PostalCodeAddress;
            existingAddress.PhoneNumber = updateAddressDto.PhoneNumber;
            existingAddress.IdCity = updateAddressDto.IdCity;
            existingAddress.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<Address>.Ok(existingAddress);
        }

        public async Task<Result<Address>> CreateAddressAsync(CreateAddressDto createAddressDto)
        {
            // Verificar cuántas direcciones activas tiene el usuario
            int activeAddressCount = await _context.Addresses
                .CountAsync(a => a.UserDocument == createAddressDto.UserDocument && a.IsActive);

            if (activeAddressCount >= 5)
                return Result<Address>.Fail("Solo puedes tener un máximo de 5 direcciones activas");

            // Codigo para validar si ya existe una dirección idéntica para ese usuario
            bool duplicateExists = await _context.Addresses.AnyAsync(a =>
                a.IsActive &&
                a.StreetAddress == createAddressDto.StreetAddress &&
                a.PostalCodeAddress == createAddressDto.PostalCodeAddress &&
                a.UserDocument == createAddressDto.UserDocument &&
                a.PhoneNumber == createAddressDto.PhoneNumber);

            if (duplicateExists)
                return Result<Address>.Fail("Esta dirección ya esta registrada");

            // Verificar si es la primera dirección activa del usuario
            bool userHasAddresses = activeAddressCount > 0;

            var address = new Address
            {
                StreetAddress = createAddressDto.StreetAddress,
                PostalCodeAddress = createAddressDto.PostalCodeAddress,
                IsDefaultAddress = !userHasAddresses,
                CountryCode = createAddressDto.CountryCode,
                AddressReference = createAddressDto.AddressReference,
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserDocument = createAddressDto.UserDocument,
                IdCity = createAddressDto.IdCity,
                PhoneNumber = createAddressDto.PhoneNumber
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

            // Si la dirección era la predeterminada, buscar una nueva para asignar
            if (address.IsDefaultAddress)
            {
                // Quitar la marca de predeterminada a la actual
                address.IsDefaultAddress = false;

                // Buscar la siguiente dirección activa más reciente del mismo usuario
                var newDefault = await _context.Addresses
                    .Where(a => a.UserDocument == address.UserDocument && a.IsActive && a.IdAddress != id)
                    .OrderByDescending(a => a.CreatedAt)
                    .FirstOrDefaultAsync();

                if (newDefault != null)
                {
                    newDefault.IsDefaultAddress = true;
                    newDefault.UpdatedAt = DateTime.Now;
                    _context.Entry(newDefault).State = EntityState.Modified;
                }
            }

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
