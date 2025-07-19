using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly BackendDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(BackendDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .ToListAsync();
        }
        
        public async Task<User> GetUserByDocumentAsync(string document)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<Result<ReadUserDto>> GetProfileAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var document = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? httpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(document))
            {
                return Result<ReadUserDto>.Fail("No se pudo obtener el documento");
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .FirstOrDefaultAsync(u => u.Document == document);

            if (user is null)
            {
                return Result<ReadUserDto>.Fail("Usuario no encontrado");
            }

            var dto = new ReadUserDto
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                RoleName = user.Role.NameRole,
                TypeDocumentName = user.TypeDocument.NameTypeDocument
            };

            return Result<ReadUserDto>.Ok(dto);
        }


        public async Task<User> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var existingUser = await _context.Users.FindAsync(updateUserDto.Document);

            if (existingUser == null) return null;

            existingUser.Username = updateUserDto.Username;

            existingUser.Email = updateUserDto.Email;

            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.PhoneNumber = updateUserDto.PhoneNumber;
            existingUser.BirthDate = updateUserDto.BirthDate;
            existingUser.IdRole = updateUserDto.IdRole;
            existingUser.IdTypeDocument = updateUserDto.IdTypeDocument;
            existingUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingUser;
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

        
        public async Task<object> FilterUsersAsync(UserFilterDto userFilterDto)
        {
            var query = _context.Users
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(userFilterDto.Document))
                query = query.Where(u => u.Document.Contains(userFilterDto.Document));

            if (!string.IsNullOrWhiteSpace(userFilterDto.Username))
                query = query.Where(u => u.Username.Contains(userFilterDto.Username));

            if (!string.IsNullOrWhiteSpace(userFilterDto.Email))
                query = query.Where(u => u.Email.Contains(userFilterDto.Email));

            if (!string.IsNullOrWhiteSpace(userFilterDto.PhoneNumber))
                query = query.Where(u => u.PhoneNumber == userFilterDto.PhoneNumber);

            if (userFilterDto.IsActive.HasValue)
                query = query.Where(u => u.IsActive == userFilterDto.IsActive.Value);

            if (userFilterDto.IdRole.HasValue)
                query = query.Where(u => u.IdRole == userFilterDto.IdRole.Value);

            if (userFilterDto.IdTypeDocument.HasValue)
                query = query.Where(u => u.IdTypeDocument == userFilterDto.IdTypeDocument.Value);

            // Ordenamiento dinamico
            if (!string.IsNullOrWhiteSpace(userFilterDto.SortBy))
            {
                query = userFilterDto.SortDesc
                    ? query.OrderByDescending(e => EF.Property<object>(e, userFilterDto.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e, userFilterDto.SortBy));
            }

            var total = await query.CountAsync();

            // Paginacion
            query = query.Skip((userFilterDto.Page - 1) * userFilterDto.PageSize)
                .Take(userFilterDto.PageSize);

            // Selección de campos especificos
            var userList = await query.ToListAsync();

            if (userFilterDto.SelectFields != null && userFilterDto.SelectFields.Any())
            {
                var selectedData = userList.Select(user =>
                {
                    var dict = new Dictionary<string, object?>();
                    foreach (var field in userFilterDto.SelectFields)
                    {
                        var prop = typeof(User).GetProperty(field);
                        if (prop != null)
                        {
                            dict[field] = prop.GetValue(user);
                        }
                    }
                    return dict;
                }).ToList();

                return new { total, data = selectedData };
            }

            return new { total, data = userList };
        }
    }
}
