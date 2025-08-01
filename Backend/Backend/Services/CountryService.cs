using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.UserDTOs;
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

        public async Task<Result<IEnumerable<ReadCountryDto>>> GetAllCountriesAsync()
        {
            var countries = await _context.Countries
                .Where(c => c.IsActive)
                .OrderBy(c => c.NameCountry)
                .Select(c => new ReadCountryDto
                {
                    IdCountry = c.IdCountry,
                    NameCountry = c.NameCountry,
                })
                .ToListAsync();

            return Result<IEnumerable<ReadCountryDto>>.Ok(countries);
        }

        public async Task<object> FilterCountriesAsync(CountryFilterDto dto)
        {
            var query = _context.Countries.AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(dto.NameCountry))
                query = query.Where(u => u.NameCountry.Contains(dto.NameCountry));

            if (dto.CreatedAt.HasValue)
                query = query.Where(u => u.CreatedAt.Date == dto.CreatedAt.Value.Date);

            if (dto.UpdatedAt.HasValue)
                query = query.Where(u => u.UpdatedAt.Value.Date == dto.UpdatedAt.Value.Date);

            if (dto.IsActive.HasValue)
                query = query.Where(u => u.IsActive == dto.IsActive.Value);

            if (dto.DeactivatedAt.HasValue)
                query = query.Where(u => u.DeactivatedAt.Value.Date == dto.DeactivatedAt.Value.Date);

            // Ordenamiento dinamico
            if (!string.IsNullOrWhiteSpace(dto.SortBy))
            {
                var property = typeof(Country).GetProperty(dto.SortBy);
                if (property != null)
                {
                    query = dto.SortDesc
                        ? query.OrderByDescending(e => EF.Property<object>(e, dto.SortBy))
                        : query.OrderBy(e => EF.Property<object>(e, dto.SortBy));
                }
            }
            else
            {
                query = query.OrderBy(e => e.NameCountry);
            }

            var total = await query.CountAsync();

            // Paginacion
            var countries = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var requiredFields = new List<string> { "idCountry", "nameCountry" };

            // Selección de campos especificos
            var selectedFields = (dto.SelectFields != null && dto.SelectFields.Any())
                ? requiredFields.Concat(dto.SelectFields).Distinct()
                : new List<string> {
                    "idCountry", "nameCountry",
                    "isActive", "createdAt", "updatedAt", "deactivatedAt"
                };

            var columns = selectedFields.Select(field => new
            {
                key = field,
                label = ToLabel(field),
                sortable = true,
                filterable = true,
                type = GetFieldType(field)
            }).ToList();

            // Selección de campos especificos
            var data = countries.Select(country =>
            {
                var dict = new Dictionary<string, object?>();

                    foreach (var field in selectedFields)
                    {
                        object? value = field switch
                        {
                            "idCountry" => country.IdCountry,
                            "nameCountry" => country.NameCountry,
                            "isActive" => country.IsActive,
                            "createdAt" => country.CreatedAt,
                            "updatedAt" => country.UpdatedAt,
                            "deactivatedAt" => country.DeactivatedAt,
                            _ => null
                        };

                        dict[field] = value;
                    }

                return dict;
            }).ToList();

            return new
            {
                total,
                page = dto.Page,
                pageSize = dto.PageSize,
                columns,
                data
            };
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

        private string ToLabel(string field) =>
            field switch
            {
                "idCountry" => "ID",
                "nameCountry" => "País",
                "isActive" => "Activo",
                "createdAt" => "Creado",
                "updatedAt" => "Actualizado",
                "deactivatedAt" => "Desactivado",
                _ => field
            };

        private string GetFieldType(string field) =>
            field switch
            {
                "idCountry" => "number",
                "isActive" => "boolean",
                "createdAt" or "updatedAt" or "deactivatedAt" => "date",
                _ => "string"
            };
    }
}