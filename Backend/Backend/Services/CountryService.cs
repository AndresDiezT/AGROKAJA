using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CountryService : ICountryService
    {
        private readonly BackendDbContext _context;

        public CountryService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Country>>> GetAllCountriesAsync()
        {
            var countries = await _context.Countries.ToListAsync();

            return Result<IEnumerable<Country>>.Ok(countries);
        }

        public async Task<Result<Country>> GetCountryByIdAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country is null)
                return Result<Country>.Fail("País no encontrado");

            return Result<Country>.Ok(country);
        }

        public async Task<Result<Country>> UpdateCountryAsync(UpdateCountryDto updateCountryDto)
        {
            var existingCountry = await _context.Countries.FindAsync(updateCountryDto.IdCountry);

            if (existingCountry is null)
                return Result<Country>.Fail("País no encontrado");

            existingCountry.NameCountry = updateCountryDto.NameCountry;
            existingCountry.CodeCountry = updateCountryDto.CodeCountry;
            existingCountry.UpdatedAt = DateTime.Now;

            _context.Entry(existingCountry).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<Country>.Ok(existingCountry);
        }

        public async Task<Result<Country>> CreateCountryAsync(CreateCountryDto createCountryDto)
        {
            if (await _context.Countries.AnyAsync(u => u.NameCountry == createCountryDto.NameCountry))
                return Result<Country>.Fail("El país ya esta registrado");

            var country = new Country
            {
                NameCountry = createCountryDto.NameCountry,
                CodeCountry = createCountryDto.CodeCountry,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Countries.Add(country);

            await _context.SaveChangesAsync();

            return Result<Country>.Ok(country);
        }

        public async Task<Result<bool>> DeactivateCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country is null)
                return Result<bool>.Fail("País no encontrado");

            if (!country.IsActive)
                return Result<bool>.Fail("El país ya esta inactivo");

            country.IsActive = false;
            country.UpdatedAt = DateTime.Now;
            country.DeactivatedAt = DateTime.Now;

            _context.Entry(country).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country is null)
                return Result<bool>.Fail("País no encontrado");

            if (country.IsActive)
                return Result<bool>.Fail("El país ya esta activo");

            country.IsActive = true;
            country.UpdatedAt = DateTime.Now;
            country.DeactivatedAt = null;

            _context.Entry(country).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}