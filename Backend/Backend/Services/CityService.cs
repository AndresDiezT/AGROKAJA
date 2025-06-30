using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CityDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class CityService : ICityService
    {
        private readonly BackendDbContext _context;

        public CityService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<City>>> GetAllCitiesAsync()
        {
            var cities = await _context.Cities
                .Include(c => c.Department)
                .ToListAsync();

            return Result<IEnumerable<City>>.Ok(cities);
        }

        public async Task<Result<City>> GetCityByIdAsync(int id)
        {
            var city = await _context.Cities
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.IdCity == id);

            if (city is null)
                return Result<City>.Fail("Ciudad no encontrada");

            return Result<City>.Ok(city);
        }

        public async Task<Result<City>> UpdateCityAsync(UpdateCityDto updateCityDto)
        {
            var existingCity = await _context.Cities.FindAsync(updateCityDto.IdCity);

            if (existingCity is null)
                return Result<City>.Fail("Ciudad no encontrada");

            existingCity.NameCity = updateCityDto.NameCity;
            existingCity.IdDepartment = updateCityDto.IdDepartment;
            existingCity.UpdatedAt = DateTime.Now;

            _context.Entry(existingCity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<City>.Ok(existingCity);
        }

        public async Task<Result<City>> CreateCityAsync(CreateCityDto createCityDto)
        {
            if (await _context.Cities.AnyAsync(u => u.NameCity == createCityDto.NameCity))
                return Result<City>.Fail("Esta ciudad ya esta registrada");

            var city = new City
            {
                NameCity = createCityDto.NameCity,
                IdDepartment = createCityDto.IdDepartment,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Cities.Add(city);

            await _context.SaveChangesAsync();

            return Result<City>.Ok(city);
        }

        public async Task<Result<bool>> DeactivateCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city is null)
                return Result<bool>.Fail("Ciudad no encontrada");

            if (!city.IsActive)
                return Result<bool>.Fail("La ciudad ya esta inactiva");

            city.IsActive = false;
            city.UpdatedAt = DateTime.Now;
            city.DeactivatedAt = DateTime.Now;

            _context.Entry(city).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city is null)
                return Result<bool>.Fail("Ciudad no encontrada");

            if (city.IsActive)
                return Result<bool>.Fail("La ciudad ya esta activa");

            city.IsActive = true;
            city.UpdatedAt = DateTime.Now;
            city.DeactivatedAt = null;

            _context.Entry(city).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
