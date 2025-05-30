using Backend.Data;
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

        public async Task<Role> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = new Role
            {
                NameRole = createRoleDto.NameRole,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return role;
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
    }
}
