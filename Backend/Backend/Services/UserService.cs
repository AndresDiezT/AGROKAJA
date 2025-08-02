using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly BackendDbContext _context;

        public UserService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.TypeDocument)
                .ToListAsync();
        }

        public async Task<User> GetUserByDocumentAsync(string document)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.TypeDocument)
                .FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<Result<string>> UpdateUserAsync(string document, UpdateUserDto updateUserDto)
        {
            var existingUser = await _context.Users.FindAsync(document);

            if (existingUser == null) return Result<string>.Fail("No se encontro el usuario");

            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.BirthDate = DateOnly.FromDateTime(updateUserDto.BirthDate);
            existingUser.Username = updateUserDto.Username;
            existingUser.PhoneNumber = updateUserDto.PhoneNumber;
            existingUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<string>.Ok("Actualizado correctamente");
        }

        public async Task<Result<bool>> VerifyEmailCodeAsync(string document, int code)
        {
            var verification = await _context.EmailVerifications
                .Where(ev => ev.UserDocument == document && !ev.IsUsed && ev.ExpiresAt > DateTime.UtcNow && ev.Code == code)
                .OrderByDescending(ev => ev.CreatedAt)
                .FirstOrDefaultAsync();

            if (verification == null)
                return Result<bool>.Fail("Código inválido o expirado");

            verification.IsUsed = true;
            _context.EmailVerifications.Update(verification);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<bool> AssignRolesToUserAsync(string document, List<int> roleIds)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Document == document);

            if (user == null) return false;

            // Limpiar roles existentes
            user.Roles.Clear();

            // Asignar nuevos roles
            foreach (var roleId in roleIds)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role != null)
                {
                    user.Roles.Add(role);
                }
            }

            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(string document)
        {
            var user = await _context.Users.FindAsync(document);

            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;
            user.DeactivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateUserAsync(string document)
        {
            var user = await _context.Users.FindAsync(document);

            if (user == null) return false;

            user.IsActive = true;
            user.UpdatedAt = DateTime.Now;
            user.DeactivatedAt = null;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<object> FilterUsersAsync(UserFilterDto dto, string userType)
        {
            var query = _context.Users
                .Include(u => u.Roles)
                .Include(u => u.TypeDocument)
                .AsQueryable();

            // Filtro por tipo de usuario
            if (userType.ToLower() == "employee")
            {
                query = query.Where(u => _context.Employees.Any(e => e.Document == u.Document));
            }
            else if (userType.ToLower() == "customer")
            {
                query = query.Where(u => _context.Customers.Any(c => c.Document == u.Document));
            }
            else
            {
                throw new ArgumentException("Tipo de usuario no válido. Usa 'employee' o 'customer'.");
            }

            // Filtros
            if (!string.IsNullOrWhiteSpace(dto.Document))
                query = query.Where(u => u.Document.Contains(dto.Document));

            if (!string.IsNullOrWhiteSpace(dto.Username))
                query = query.Where(u => u.Username.Contains(dto.Username));

            if (!string.IsNullOrWhiteSpace(dto.Email))
                query = query.Where(u => u.Email.Contains(dto.Email));

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                query = query.Where(u => u.PhoneNumber == dto.PhoneNumber);

            if (dto.CreatedAt.HasValue)
                query = query.Where(u => u.CreatedAt.Date == dto.CreatedAt.Value.Date);

            if (dto.UpdatedAt.HasValue)
                query = query.Where(u => u.UpdatedAt.Value.Date == dto.UpdatedAt.Value.Date);

            if (dto.IsActive.HasValue)
                query = query.Where(u => u.IsActive == dto.IsActive.Value);

            if (dto.DeactivatedAt.HasValue)
                query = query.Where(u => u.DeactivatedAt.Value.Date == dto.DeactivatedAt.Value.Date); ;

            if (dto.IdRole.HasValue)
                query = query.Where(u => u.Roles.Any(r => r.IdRole == dto.IdRole.Value));

            if (dto.IdTypeDocument.HasValue)
                query = query.Where(u => u.IdTypeDocument == dto.IdTypeDocument.Value);

            // Ordenamiento dinamico
            if (!string.IsNullOrWhiteSpace(dto.SortBy))
            {
                query = dto.SortDesc
                    ? query.OrderByDescending(e => EF.Property<object>(e, dto.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e, dto.SortBy));
            }

            var total = await query.CountAsync();

            // Paginacion
            var users = await query
                .Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            var requiredFields = new List<string> { "document" };

            var selectedFields = (dto.SelectFields != null && dto.SelectFields.Any())
                ? requiredFields.Concat(dto.SelectFields).Distinct()
                : new List<string> {
                    "document", "username", "email", "firstName", "lastName", "phoneNumber", "birthDate",
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
            var data = users.Select(user =>
            {
                var dict = new Dictionary<string, object?>();

                foreach (var field in selectedFields)
                {
                    object? value = field switch
                    {
                        "document" => user.Document,
                        "username" => user.Username,
                        "email" => user.Email,
                        "firstName" => user.FirstName,
                        "lastName" => user.LastName,
                        "phoneNumber" => user.PhoneNumber,
                        "birthDate" => user.BirthDate,
                        "roleName" => user.Roles.FirstOrDefault().NameRole,
                        "typeDocumentName" => user.TypeDocument.NameTypeDocument,
                        "isActive" => user.IsActive,
                        "createdAt" => user.CreatedAt,
                        "updatedAt" => user.UpdatedAt,
                        "deactivatedAt" => user.DeactivatedAt,
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

        private string ToLabel(string field) =>
            field switch
            {
                "document" => "Documento",
                "username" => "Usuario",
                "email" => "Correo",
                "firstName" => "Nombre",
                "lastName" => "Apellidos",
                "phoneNumber" => "Telefono",
                "birthDate" => "Nombres",
                "roleName" => "Rol",
                "typeDocumentName" => "Tipo Documento",
                "isActive" => "Activo",
                "createdAt" => "Creado",
                "updatedAt" => "Actualizado",
                "deactivatedAt" => "Desactivado",
                _ => field
            };

        private string GetFieldType(string field) =>
            field switch
            {
                "isActive" => "boolean",
                "createdAt" or "updatedAt" or "deactivatedAt" or "birthDate" => "date",
                _ => "string"
            };
    }
}
