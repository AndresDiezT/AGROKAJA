using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class RoleService : IRoleService
    {
        private readonly BackendDbContext _context;

        public RoleService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<object> FilterRolesAsync(RoleFilterDto dto)
        {
            var query = _context.Roles.AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(dto.NameRole))
                query = query.Where(u => u.NameRole.Contains(dto.NameRole));

            if (!string.IsNullOrWhiteSpace(dto.GuardNameRole))
                query = query.Where(u => u.GuardNameRole.Contains(dto.GuardNameRole));

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
                query = query.OrderBy(e => e.NameRole);
            }

            var total = await query.CountAsync();

            // Paginacion
            var roles = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var requiredFields = new List<string> { "idRole", "nameRole" };

            // Selección de campos especificos
            var selectedFields = (dto.SelectFields != null && dto.SelectFields.Any())
                ? requiredFields.Concat(dto.SelectFields).Distinct()
                : new List<string> {
                    "idRole", "nameRole", "guardNameRole",
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
            var data = roles.Select(country =>
            {
                var dict = new Dictionary<string, object?>();

                foreach (var field in selectedFields)
                {
                    object? value = field switch
                    {
                        "idRole" => country.IdRole,
                        "nameRole" => country.NameRole,
                        "guardNameRole" => country.GuardNameRole,
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

        public async Task<Result<string>> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            // Obtener permisos comunes
            var commonPermissions = await _context.Permissions
                 .Where(p => p.NamePermission.StartsWith("common."))
                 .ToListAsync();

            var existingRole = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.NameRole == createRoleDto.NameRole);

            if (existingRole != null)
            {
                return Result<string>.Fail("Ya existe un rol con este nombre");
            }

            // Crear role y asignar permisos comunes
            var role = new Role
            {
                NameRole = createRoleDto.NameRole,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Permissions = commonPermissions,
                GuardNameRole = "web",
                EditableRole = true,
            };

            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return Result<string>.Ok("Role creado correctamente");
        }

        public async Task<Role?> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            var existingRole = await _context.Roles.FindAsync(updateRoleDto.IdRole);

            if (existingRole == null) return null;

            existingRole.NameRole = updateRoleDto.NameRole;
            existingRole.UpdatedAt = DateTime.Now;

            _context.Entry(existingRole).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existingRole;
        }

        public async Task<bool> DeactivateRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null || !role.IsActive) return false;

            role.IsActive = false;
            role.UpdatedAt = DateTime.Now;
            role.DeactivatedAt = DateTime.Now;

            _context.Entry(role).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null || role.IsActive) return false;

            role.IsActive = true;
            role.UpdatedAt = DateTime.Now;
            role.DeactivatedAt = null;

            _context.Entry(role).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        private string ToLabel(string field) =>
            field switch
            {
                "idRole" => "ID",
                "nameRole" => "Role",
                "guardNameRole" => "Tipo",
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
