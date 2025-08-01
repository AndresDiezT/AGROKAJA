using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CityDTOs;
using Backend.DTOs.DepartmentDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly BackendDbContext _context;

        public DepartmentService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ReadDepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Where(c => c.IsActive)
                .OrderBy(c => c.NameDepartment)
                .Select(c => new ReadDepartmentDto
                {
                    IdDepartment = c.IdDepartment,
                    NameDepartment = c.NameDepartment,
                    IdCountry = c.IdCountry,
                })
                .ToListAsync();

            return Result<IEnumerable<ReadDepartmentDto>>.Ok(departments);
        }

        public async Task<object> FilterDepartmentsAsync(DepartmentFilterDto dto)
        {
            var query = _context.Departments
                .Include(c => c.Country)
                .AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(dto.NameDepartment))
                query = query.Where(u => u.NameDepartment.Contains(dto.NameDepartment));

            if (dto.IdCountry.HasValue)
                query = query.Where(u => u.Country.IdCountry == dto.IdCountry.Value);

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
                    if (dto.SortBy == "CountryName")
                    {
                        query = dto.SortDesc
                            ? query.OrderByDescending(e => e.Country.NameCountry)
                            : query.OrderBy(e => e.Country.NameCountry);
                    }
                    else
                    {
                        query = query.OrderBy(e => e.NameDepartment);
                    }
                }
            }
            else
            {
                query = query.OrderBy(e => e.NameDepartment);
            }

            var total = await query.CountAsync();

            // Paginacion
            var departments = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var requiredFields = new List<string> { "idDepartment", "nameDepartment", "nameCountry", "isActive" };

            var selectedFields = (dto.SelectFields != null && dto.SelectFields.Any())
                ? requiredFields.Concat(dto.SelectFields).Distinct()
                : new List<string> {
                    "idDepartment", "nameDepartment", "nameCountry",
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

            var data = departments.Select(department =>
            {
                var dict = new Dictionary<string, object?>();

                foreach (var field in selectedFields)
                {
                    object? value = field switch
                    {
                        "idDepartment" => department.IdDepartment,
                        "nameDepartment" => department.NameDepartment,
                        "nameCountry" => department.Country?.NameCountry,
                        "isActive" => department.IsActive,
                        "createdAt" => department.CreatedAt,
                        "updatedAt" => department.UpdatedAt,
                        "deactivatedAt" => department.DeactivatedAt,
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

        public async Task<Result<Department>> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Country)
                .FirstOrDefaultAsync(d => d.IdDepartment == id);

            if (department is null)
                return Result<Department>.Fail("Departamento no encontrado");

            return Result<Department>.Ok(department);
        }

        public async Task<Result<Department>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            if (await _context.Departments.AnyAsync(u => u.NameDepartment == createDepartmentDto.NameDepartment))
                return Result<Department>.Fail("El departamento ya esta registrado");

            var department = new Department
            {
                NameDepartment = createDepartmentDto.NameDepartment,
                IdCountry = createDepartmentDto.IdCountry,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Departments.Add(department);

            await _context.SaveChangesAsync();

            return Result<Department>.Ok(department);
        }

        public async Task<Result<Department>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            var existingDepartment = await _context.Departments.FindAsync(updateDepartmentDto.IdDepartment);

            if (existingDepartment is null)
                return Result<Department>.Fail("Departamento no encontrado");

            existingDepartment.NameDepartment = updateDepartmentDto.NameDepartment;
            existingDepartment.IdCountry = updateDepartmentDto.IdCountry;
            existingDepartment.UpdatedAt = DateTime.Now;

            _context.Entry(existingDepartment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<Department>.Ok(existingDepartment);
        }

        public async Task<Result<bool>> DeactivateDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department is null)
                return Result<bool>.Fail("Departamento no encontrado");

            if (!department.IsActive)
                return Result<bool>.Fail("El departamento ya esta inactivo");

            department.IsActive = false;
            department.UpdatedAt = DateTime.Now;
            department.DeactivatedAt = DateTime.Now;

            _context.Entry(department).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department is null)
                return Result<bool>.Fail("Departamento no encontrado");

            if (department.IsActive)
                return Result<bool>.Fail("El departamento ya esta activo");

            department.IsActive = true;
            department.UpdatedAt = DateTime.Now;
            department.DeactivatedAt = null;

            _context.Entry(department).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        private string ToLabel(string field) =>
            field switch
            {
                "idDepartment" => "ID",
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
                "idDepartment" => "number",
                "isActive" => "boolean",
                "createdAt" or "updatedAt" or "deactivatedAt" => "date",
                _ => "string"
            };
    }
}
