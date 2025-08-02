using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CityDTOs;
using Backend.DTOs.CountryDTOs;
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

        public async Task<Result<IEnumerable<ReadCityDto>>> GetAllCitiesAsync()
        {
            var cities = await _context.Cities
                .Where(c => c.IsActive)
                .OrderBy(c => c.NameCity)
                .Select(c => new ReadCityDto
                {
                    IdCity = c.IdCity,
                    NameCity = c.NameCity,
                    IdDepartment = c.IdDepartment,
                })
                .ToListAsync();

            return Result<IEnumerable<ReadCityDto>>.Ok(cities);
        }

        public async Task<object> FilterCitiesAsync(CityFilterDto dto)
        {
            var query = _context.Cities
                .Include(c => c.Department)
                .ThenInclude(s => s.Country)
                .AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(dto.NameCity))
                query = query.Where(u => u.NameCity.Contains(dto.NameCity));

            if (dto.IdCountry.HasValue)
                query = query.Where(u => u.Department.Country.IdCountry == dto.IdCountry.Value);

            if (dto.IdDepartment.HasValue)
                query = query.Where(u => u.Department.IdDepartment == dto.IdDepartment.Value);

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
                var property = typeof(City).GetProperty(dto.SortBy);
                if (property != null)
                {
                    query = dto.SortDesc
                        ? query.OrderByDescending(e => EF.Property<object>(e, dto.SortBy))
                        : query.OrderBy(e => EF.Property<object>(e, dto.SortBy));
                }
                else
                {
                    if (dto.SortBy == "DepartmentName")
                    {
                        query = dto.SortDesc
                            ? query.OrderByDescending(e => e.Department.NameDepartment)
                            : query.OrderBy(e => e.Department.NameDepartment);
                    }
                    else if (dto.SortBy == "CountryName")
                    {
                        query = dto.SortDesc
                            ? query.OrderByDescending(e => e.Department.Country.NameCountry)
                            : query.OrderBy(e => e.Department.Country.NameCountry);
                    }
                    else
                    {
                        query = query.OrderBy(e => e.NameCity);
                    }
                }
            }
            else
            {
                query = query.OrderBy(e => e.NameCity);
            }

            var total = await query.CountAsync();

            // Paginacion
            var cities = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var requiredFields = new List<string> { "idCity", "nameCity", "nameDepartment", "isActive" };

            var selectedFields = (dto.SelectFields != null && dto.SelectFields.Any())
                ? requiredFields.Concat(dto.SelectFields).Distinct()
                : new List<string> {
                    "idCity", "nameCity", "nameDepartment",
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
            var data = cities.Select(city =>
            {
                var dict = new Dictionary<string, object?>();

                    foreach (var field in selectedFields)
                    {
                        object? value = field switch
                        {
                            "idCity" => city.IdCity,
                            "nameCity" => city.NameCity,
                            "nameDepartment" => city.Department?.NameDepartment,
                            "nameCountry" => city.Department?.Country?.NameCountry,
                            "isActive" => city.IsActive,
                            "createdAt" => city.CreatedAt,
                            "updatedAt" => city.UpdatedAt,
                            "deactivatedAt" => city.DeactivatedAt,
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

        private string ToLabel(string field) =>
            field switch
            {
                "idCity" => "ID",
                "nameCity" => "Ciudad",
                "nameDepartment" => "Departamento",
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
                "idCity" => "number",
                "isActive" => "boolean",
                "createdAt" or "updatedAt" or "deactivatedAt" => "date",
                _ => "string"
            };
    }
}
