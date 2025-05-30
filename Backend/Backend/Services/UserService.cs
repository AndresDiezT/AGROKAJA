using Backend.Data;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .ToListAsync();
        }

        public async Task<User?> GetUserByDocumentAsync(string document)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TypeDocument)
                .FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
        {
            var newUser = new User
            {
                Document = createUserDto.Document,
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Email, // NO OLVIDAR HASHEARRRRRRRRRR
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                PhoneNumber = createUserDto.PhoneNumber,
                BirthDate = createUserDto.BirthDate,
                CreatedAt = DateTime.Now,
                IsActive = true,
                RoleId = createUserDto.RoleId,
                IdTypeDocument = createUserDto.IdTypeDocument
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<User?> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var existingUser = await _context.Users.FindAsync(updateUserDto.Document);

            if (existingUser == null) return null;

            existingUser.Username = updateUserDto.Username;

            existingUser.Email = updateUserDto.Email;

            //if (!string.IsNullOrEmpty(updateUserDto.Password))
            //{
            //    existingUser.PasswordHash = HashPassword(updateUserDto.Password);
            //}
            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.PhoneNumber = updateUserDto.PhoneNumber;
            existingUser.BirthDate = updateUserDto.BirthDate;
            existingUser.RoleId = updateUserDto.RoleId;
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

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
